using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("_Accessory/Default")]
[RequireComponent(typeof(Rigidbody), typeof(CenterOfMass))]
public class Accessory : MonoBehaviour
{
    protected Example example;
    protected Rigidbody rb;
    protected CenterOfMass centerOfMass;
    protected bool equipped;
    protected List<CenterOfMass> collidedObjects;

    protected Vector3 fixedDistance;
    protected Quaternion fixedRotation;
    protected IEnumerator setFixedDistance;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        centerOfMass = GetComponent<CenterOfMass>();
        rb.centerOfMass = centerOfMass.centerOfMass;
        rb.mass = centerOfMass.mass;

        collidedObjects = new List<CenterOfMass>();

        fixedDistance = new Vector3(0.0005f, 0, 0);
        fixedRotation = Quaternion.Euler(0, -90, 0);
        setFixedDistance = SetFixedDistance();
    }

    protected virtual void Update()
    {
        if (equipped) HoldFixedDistance();
    }

    public virtual void FirstAction()
    {
    }

    public virtual void SecondAction()
    {
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) return;
        CenterOfMass cm = collision.gameObject.GetComponent<CenterOfMass>();
        if (cm)
        {
            collidedObjects.Add(cm);
            if (equipped) example.generalCenterOfMass.list.Add(cm);
        }
    }

    protected virtual void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) return;
        CenterOfMass cm = collision.gameObject.GetComponent<CenterOfMass>();
        if (cm)
        {
            collidedObjects.Remove(cm);
            if (equipped) example.generalCenterOfMass.list.Remove(cm);
        }
    }

    public virtual void Equip(Transform parent, Example ex)
    {
        transform.parent = parent;
        example = ex;
        example.generalCenterOfMass.list.Add(centerOfMass);
        foreach (CenterOfMass cm in collidedObjects)
        {
            example.generalCenterOfMass.list.Add(cm);
        }
        rb.useGravity = false;

        rb.velocity = Vector3.zero;

        StopCoroutine(setFixedDistance);
        setFixedDistance = SetFixedDistance();
        StartCoroutine(setFixedDistance);
    }

    public virtual void Unequip()
    {
        example.generalCenterOfMass.list.Remove(centerOfMass);
        foreach (CenterOfMass cm in collidedObjects)
        {
            example.generalCenterOfMass.list.Remove(cm);
        }
        transform.parent = null;
        rb.useGravity = true;

        example = null;
        equipped = false;
    }

    protected void HoldFixedDistance()
    {
        transform.localPosition = fixedDistance;
        transform.localRotation = fixedRotation;
        rb.velocity = Vector3.zero;
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
