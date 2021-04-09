using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : ArticulationBodyCenterOfMass
{
    [Space(10, order = 0), Header("�������", order = 1)]
    public bool alwaysAllowRotations;
    [Space(10, order = 0), Header("��������", order = 1)]
    public float startLinearSpeed;
    public float startAngularSpeed;
    private float currentLinearSpeed;
    private float currentAngularSpeed;
    [Space(10)]
    public NonPlayerCollisionCounter caterpillarLeftCollisionCounter;
    public NonPlayerCollisionCounter caterpillarRightCollisionCounter;
    public List<NonPlayerCollisionCounter> legsCollisionCounters;
    [Space(10)]
    public List<ArticulationBodyXDriveModification> articulationBodyRotations;
    public List<ArticulationBodyXDriveModification> articulationBodyLegs;
    private bool legsTouchSurface;
    public bool MovementAllowed { get; private set; } = false;
    public bool RotationsAllowed { get; private set; } = false;
    [Space(10, order = 0), Header("�������� ������������ � ��������", order = 1)]
    public AccessoryJoinPoint accessoryJoinPoint;
    [Space(10, order = 0), Header("������� �����", order = 1)]
    public Transform regularCamerasContainer;
    public Transform specialCamerasContainer;
    [Space(10, order = 0), Header("���������", order = 1)]
    public RobotState defaultState;
    public float timeForSettingState = 2f;
    private List<Collider> childrenColliders;
    private bool stateIsSet = true;
    private IEnumerator setStateWithDelayCoroutine;

    protected override void Awake()
    {
        base.Awake();
        currentLinearSpeed = startLinearSpeed * Time.fixedDeltaTime;
        currentAngularSpeed = startAngularSpeed * Time.fixedDeltaTime;
        childrenColliders = new List<Collider>(transform.GetComponentsInChildren<Collider>());
        setStateWithDelayCoroutine = SetStateWithDelayCoroutine(defaultState);
    }

    private void FixedUpdate()
    {
        if (stateIsSet)
        {
            legsTouchSurface = LegsTouchSurface();
            RotationsAllowed = alwaysAllowRotations || (legsTouchSurface && accessoryJoinPoint.IsFree);
            AllowRotations(articulationBodyRotations, RotationsAllowed);
            MovementAllowed = accessoryJoinPoint.IsFree && (caterpillarLeftCollisionCounter.HasCollisions || caterpillarRightCollisionCounter.HasCollisions) && !legsTouchSurface;
            if (MovementAllowed)
            {
                articulationBody.AddForce(transform.forward * Input.GetAxis("Vertical") * currentLinearSpeed * accessoryJoinPoint.SpeedModifier);
                articulationBody.AddTorque(transform.up * Input.GetAxis("Horizontal") * currentAngularSpeed * accessoryJoinPoint.SpeedModifier);
            }
        }
    }

    private void AllowRotations(List<ArticulationBodyXDriveModification> xDriveModifications, bool value)
    {
        foreach (ArticulationBodyXDriveModification abr in xDriveModifications)
        {
            abr.AllowRotation(value);
        }
    }

    private bool LegsTouchSurface()
    {
        int legsCollisionsCount = 0;
        foreach (NonPlayerCollisionCounter leg in legsCollisionCounters)
        {
            if (leg.HasCollisions) legsCollisionsCount++;
        }
        return legsCollisionsCount == legsCollisionCounters.Count;
    }

    private void ApplyState(RobotState newState)
    {
        stateIsSet = false;
        MovementAllowed = false;
        RotationsAllowed = false;
        AllowRotations(articulationBodyRotations, stateIsSet);
        AllowRotations(articulationBodyLegs, stateIsSet);
        for (int i = 0; i < articulationBodyRotations.Count; ++i)
        {
            articulationBodyRotations[i].MoveTo(newState.armStatesValues[i].value);
        }
        for (int i = 0; i < articulationBodyLegs.Count; ++i)
        {
            articulationBodyLegs[i].MoveTo(newState.legsStatesValues[i].value);
        }

        articulationBody.TeleportRoot(newState.statePoint.position, newState.statePoint.rotation);
        articulationBody.immovable = true;
        foreach (Collider collider in childrenColliders)
        {
            collider.enabled = false;
        }
    }

    private void UnlockMovement()
    {
        articulationBody.immovable = false;
        foreach (Collider collider in childrenColliders)
        {
            collider.enabled = true;
        }
        AllowRotations(articulationBodyLegs, true);
        stateIsSet = true;
    }

    private IEnumerator SetStateWithDelayCoroutine(RobotState state)
    {
        ApplyState(state);
        yield return new WaitForSeconds(timeForSettingState);
        UnlockMovement();
    }

    private void ApplyStateWithDelay(RobotState state)
    {
        StopCoroutine(setStateWithDelayCoroutine);
        setStateWithDelayCoroutine = SetStateWithDelayCoroutine(state);
        StartCoroutine(setStateWithDelayCoroutine);
    }

    public void SetState(RobotState specialState)
    {
        if (specialState) ApplyStateWithDelay(specialState);
        else ApplyStateWithDelay(defaultState);
    }

    public ArticulationBodyXDriveModification GetXDriveModificationByIndex(int index, bool getFromLegsList = false)
    {
        if (index < 0) return null;
        else
        {
            if (getFromLegsList)
            {
                return (index < articulationBodyLegs.Count) ? articulationBodyLegs[index] : null;
            }
            else
            {
                return (index < articulationBodyRotations.Count) ? articulationBodyRotations[index] : null;
            }
        }
    }
}