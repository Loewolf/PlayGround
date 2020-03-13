using System.Collections;
using UnityEngine;
[AddComponentMenu("_Accessory/Default")]
public class Accessory : AttachableObject
{
    protected Example example;
    protected bool equipped;

    protected Vector3 fixedDistance;
    protected Quaternion fixedRotation;
    protected IEnumerator setFixedDistance;

    protected virtual void Start()
    {
        rigidbodyTaker.centerOfMass = centerOfMass;
        rigidbodyTaker.mass = mass;

        fixedDistance = new Vector3(0.0005f, 0, 0);
        fixedRotation = Quaternion.Euler(0, -90, 0);
        setFixedDistance = SetFixedDistance();
    }

    protected virtual void Update()
    {
    }

    public virtual void FirstAction()
    {
    }

    public virtual void SecondAction()
    {
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
    }

    protected virtual void OnCollisionExit(Collision collision)
    {
    }

    public virtual void Equip(Transform parent, Example ex)
    {
        transform.parent = parent;
        example = ex;
        example.generalCenterOfMass.list.Add(this);
        LeaveTaker(example);

        StopCoroutine(setFixedDistance);
        setFixedDistance = SetFixedDistance();
        StartCoroutine(setFixedDistance);
    }

    public virtual void Unequip()
    {
        example.generalCenterOfMass.list.Remove(this);
        ReturnToTaker(example);

        example = null;
        equipped = false;
    }

    protected IEnumerator SetFixedDistance()
    {
        Vector3 startPosition = transform.localPosition;
        Quaternion startRotation = transform.localRotation;
        Quaternion endRotation = Quaternion.Euler(fixedRotation.eulerAngles.x, fixedRotation.eulerAngles.y, startRotation.eulerAngles.z);
        for (float f = 0.1f; f <= 1; f += 0.1f)
        {
            transform.localPosition = Vector3.Lerp(startPosition, fixedDistance, f);
            transform.localRotation = Quaternion.Lerp(startRotation, endRotation, f);
            yield return new WaitForFixedUpdate();
        }
        fixedRotation = endRotation;
        equipped = true;
    }
}
