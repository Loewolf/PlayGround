using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("_Accessory/Grab")]
public class Grab : Accessory
{
    public GameObject leftClaw;
    public GameObject rightClaw;
    public float speed;
    public float topRotationBorderDegrees;
    public float lowRotationBorderDegrees;
    public List<AttachableObject> Left;
    public List<AttachableObject> Right;

    private bool attached;
    private AttachableObject attachedObject;
    private float rot; // by Y axis
    private float attachedRot;

    protected override void Start()
    {
        rigidbodyTaker.centerOfMass = centerOfMass;
        rigidbodyTaker.mass = mass;

        Left = new List<AttachableObject>();
        Right = new List<AttachableObject>();

        fixedDistance = new Vector3(0.0005f, 0, 0);
        fixedRotation = Quaternion.Euler(0, -90, 0);
        setFixedDistance = SetFixedDistance();

        attached = false;
    }

    protected override void Update()
    {
        if (equipped&&!attached)
        {
            foreach (AttachableObject rb_left in Left)
            {
                foreach (AttachableObject rb_right in Right)
                {
                    if (rb_left && rb_left == rb_right && rb_left != example.rb)
                    {
                        SetAttached(rb_left);

                        Left.Remove(rb_left);
                        Right.Remove(rb_right);
                        break;
                    }
                }
                if (attached) break;
            }
        }
    }

    public override void FirstAction()
    {
        if (!attached)
        {
            rot += Time.deltaTime * speed;
            RotateClaws();
        }
    }

    public override void SecondAction()
    {
        rot -= Time.deltaTime * speed;
        RotateClaws();
        if (attached)
        {
            if (rot + 0.5f < attachedRot || rot == lowRotationBorderDegrees)
                SetFree();
        }
    }

    private void RotateClaws()
    {
        if (rot > topRotationBorderDegrees) rot = topRotationBorderDegrees;
        else if (rot < lowRotationBorderDegrees) rot = lowRotationBorderDegrees;
        leftClaw.transform.localRotation = Quaternion.Euler(0, rot, 0);
        rightClaw.transform.localRotation = Quaternion.Euler(0, -rot, 0);
    }


    public void SetAttached(AttachableObject ao)
    {
        ao.transform.parent = transform;
        ao.LeaveTaker();
        attachedObject = ao;
        if (equipped) example.generalCenterOfMass.list.Add(ao);
        attached = true;
        attachedRot = rot;
    }

    public void SetFree()
    {
        attachedObject.ReturnToTaker();
        if (equipped) example.generalCenterOfMass.list.Remove(attachedObject);
        attachedObject = null;
        attached = false;
    }

    public override void Equip(Transform parent, Example ex)
    {
        transform.parent = parent;
        example = ex;
        example.generalCenterOfMass.list.Add(this);
        if (attached)
        {
            example.generalCenterOfMass.list.Add(attachedObject);
        }
        LeaveTaker();

        StopCoroutine(setFixedDistance);
        setFixedDistance = SetFixedDistance();
        StartCoroutine(setFixedDistance);
    }

    public override void Unequip()
    {
        example.generalCenterOfMass.list.Remove(this);
        if (attached)
        {
            example.generalCenterOfMass.list.Remove(attachedObject);
            SetFree();
        }
        ReturnToTaker();

        example = null;
        equipped = false;
    }
}
