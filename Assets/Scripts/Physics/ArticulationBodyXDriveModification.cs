using UnityEngine;
[RequireComponent(typeof(ArticulationBody))]
public class ArticulationBodyXDriveModification : MonoBehaviour
{
    public float speed = 90f;
    [Space(10)]
    public KeyCode decreaseValueButton;
    public KeyCode increaseValueButton;

    private ArticulationBody articulationBody;
    private ArticulationDrive xDrive;
    private float fixedSpeed;
    private float angleModifier;
    private bool rotationAllowed = true;
    private bool inRadians;
    private Vector3 startPosition;
    private Quaternion startRotation;

    private void Awake()
    {
        articulationBody = GetComponent<ArticulationBody>();
        fixedSpeed = speed * Time.fixedDeltaTime;
        inRadians = articulationBody.jointType != ArticulationJointType.PrismaticJoint;
        angleModifier = inRadians ? Mathf.Rad2Deg : 1f;
        startPosition = transform.localPosition;
        startRotation = transform.localRotation;
    }

    private void FixedUpdate()
    {
        if (rotationAllowed)
        {
            if (Input.GetKey(decreaseValueButton))
            {
                OnDecreaseAction();
            }

            if (Input.GetKey(increaseValueButton))
            {
                OnIncreaseAction();
            }
        }
    }

    private void OnDecreaseAction()
    {
        MoveTo(articulationBody.jointPosition[0] * angleModifier - fixedSpeed);
    }

    private void OnIncreaseAction()
    {
        MoveTo(articulationBody.jointPosition[0] * angleModifier + fixedSpeed);
    }

    public void MoveTo(float value)
    {
        xDrive = articulationBody.xDrive;
        xDrive.target = value;
        articulationBody.xDrive = xDrive;
    }

    public void AllowRotation(bool value)
    {
        rotationAllowed = value;
    }

    private void SetZeroVelocity()
    {
        articulationBody.velocity = Vector3.zero;
        articulationBody.angularVelocity = Vector3.zero;
    }
}