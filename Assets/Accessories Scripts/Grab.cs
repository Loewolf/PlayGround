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
    [HideInInspector] public List<RigidbodyCustomCenterOfMass> Left;
    [HideInInspector] public List<RigidbodyCustomCenterOfMass> Right;
    [System.Serializable]
    public class AttachedObject
    {
        public Vector3 fixedPosition;
        public Quaternion fixedRotation;
        public bool triggered;
        public bool collided;

        public AttachedObject(Vector3 newFixedPosition, Quaternion newFixedRotation)
        {
            fixedPosition = newFixedPosition;
            fixedRotation = newFixedRotation;
        }

        public AttachedObject()
        {
        }

        public void SetCollided(bool value)
        {
            collided = value;
        }

        public void SetTriggered(bool value)
        {
            triggered = value;
        }

        public bool IsTriggeredOrCollided()
        {
            return (triggered || collided);
        }

        public void SetPositionAndRotation(Vector3 pos, Quaternion rot)
        {
            fixedPosition = pos;
            fixedRotation = rot;
        }
    }

    private bool attached;
    private Dictionary<RigidbodyCustomCenterOfMass, AttachedObject> attachedRigidbody;
    private int triggeredCount = 0;
    private float rot; // by Y axis

    protected override void Start()
    {
        rb = GetComponent<Rigidbody>();
        centerOfMass = GetComponent<CenterOfMass>();
        rb.centerOfMass = centerOfMass.centerOfMass;
        rb.mass = centerOfMass.mass;

        collidedObjects = new List<CenterOfMass>();

        Left = new List<RigidbodyCustomCenterOfMass>();
        Right = new List<RigidbodyCustomCenterOfMass>();

        fixedDistance = new Vector3(0.0005f, 0, 0);
        fixedRotation = Quaternion.Euler(0, -90, 0);
        setFixedDistance = SetFixedDistance();

        attached = false;
        attachedRigidbody = new Dictionary<RigidbodyCustomCenterOfMass, AttachedObject>();
    }

    protected override void Update()
    {
        if (equipped)
        {
            HoldFixedDistance();

            foreach (RigidbodyCustomCenterOfMass rb_left in Left)
            {
                foreach (RigidbodyCustomCenterOfMass rb_right in Right)
                {
                    if (rb_left == rb_right && rb_left != rb)
                    {
                        if (!attachedRigidbody.ContainsKey(rb_left))
                        {
                            SetAttachedFull(rb_left);
                        }
                        else if (!attachedRigidbody[rb_left].triggered)
                        {
                            SetAttachedPart(rb_left);
                        }
                    }
                }
            }

            foreach (RigidbodyCustomCenterOfMass attached_rb in attachedRigidbody.Keys)
            {
                if (attachedRigidbody[attached_rb].triggered)
                {
                    attached_rb.transform.localPosition = attachedRigidbody[attached_rb].fixedPosition;
                    attached_rb.transform.localRotation = attachedRigidbody[attached_rb].fixedRotation;
                }
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
    }

    private void RotateClaws()
    {
        if (rot > topRotationBorderDegrees) rot = topRotationBorderDegrees;
        else if (rot < lowRotationBorderDegrees) rot = lowRotationBorderDegrees;
        leftClaw.transform.localRotation = Quaternion.Euler(0, rot, 0);
        rightClaw.transform.localRotation = Quaternion.Euler(0, -rot, 0);
    }


    public void SetAttachedFull(RigidbodyCustomCenterOfMass cm)
    {
        cm.rb.useGravity = false;
        cm.transform.parent = transform;
        attachedRigidbody.Add(cm, new AttachedObject(cm.transform.localPosition, cm.transform.localRotation));
        if (equipped) example.generalCenterOfMass.list.Add(cm.cm);
        attachedRigidbody[cm].SetTriggered(true);
        triggeredCount++;
        attached = true;
    }

    public void SetAttachedPart(RigidbodyCustomCenterOfMass cm)
    {
        cm.rb.useGravity = false;
        cm.transform.parent = transform;
        attachedRigidbody[cm].SetPositionAndRotation(cm.transform.localPosition, cm.transform.localRotation);
        attachedRigidbody[cm].SetTriggered(true);
        triggeredCount++;
        attached = true;
    }

    public void SetFree(RigidbodyCustomCenterOfMass cm)
    {
        cm.rb.useGravity = true;
        cm.transform.parent = null;
        if (attachedRigidbody.ContainsKey(cm))
        {
            if (attachedRigidbody[cm].triggered)
            {
                attachedRigidbody[cm].SetTriggered(false);
                triggeredCount--;
            }
            if (!attachedRigidbody[cm].collided)
            {
                attachedRigidbody.Remove(cm);
                if (equipped) example.generalCenterOfMass.list.Remove(cm.cm);
            }
            if (triggeredCount == 0) attached = false;
        }

    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) return;
        RigidbodyCustomCenterOfMass cm = collision.gameObject.GetComponent<RigidbodyCustomCenterOfMass>();
        if (cm)
        {
            if (!attachedRigidbody.ContainsKey(cm))
            {
                attachedRigidbody.Add(cm, new AttachedObject());
                if (equipped) example.generalCenterOfMass.list.Add(cm.cm);
            }
            attachedRigidbody[cm].SetCollided(true);
        }
    }

    protected override void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) return;
        RigidbodyCustomCenterOfMass cm = collision.gameObject.GetComponent<RigidbodyCustomCenterOfMass>();
        if (cm)
        {
            if (attachedRigidbody.ContainsKey(cm))
            {
                attachedRigidbody[cm].SetCollided(false);
                if (!attachedRigidbody[cm].IsTriggeredOrCollided())
                {
                    attachedRigidbody.Remove(cm);
                    if (equipped) example.generalCenterOfMass.list.Remove(cm.cm);
                }
            }
            else
            {
                if (equipped) example.generalCenterOfMass.list.Remove(cm.cm);
            }
        }
    }

    public override void Equip(Transform parent, Example ex)
    {
        transform.parent = parent;
        example = ex;
        example.generalCenterOfMass.list.Add(centerOfMass);
        foreach (RigidbodyCustomCenterOfMass cm in attachedRigidbody.Keys)
        {
            example.generalCenterOfMass.list.Add(cm.cm);
        }
        rb.useGravity = false;

        rb.velocity = Vector3.zero;

        StopCoroutine(setFixedDistance);
        setFixedDistance = SetFixedDistance();
        StartCoroutine(setFixedDistance);
    }

    public override void Unequip()
    {
        example.generalCenterOfMass.list.Remove(centerOfMass);
        foreach (RigidbodyCustomCenterOfMass cm in attachedRigidbody.Keys)
        {
            example.generalCenterOfMass.list.Remove(cm.cm);
            SetFree(cm);
        }
        transform.parent = null;
        rb.useGravity = true;

        example = null;
        equipped = false;
    }
}
