using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("_Accessory/Default")]
[RequireComponent(typeof(Rigidbody), typeof(CenterOfMass))]
public class Accessory : MonoBehaviour
{
    protected Rigidbody rb;
    protected CenterOfMass centerOfMass;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        centerOfMass = GetComponent<CenterOfMass>();
        rb.centerOfMass = centerOfMass.centerOfMass;
        rb.mass = centerOfMass.mass;
    }

    public virtual void FirstAction()
    {
        Debug.Log("First Action is Empty");
    }

    public virtual void SecondAction()
    {
        Debug.Log("Second Action is Empty");
    }

    public void Equip(Transform parent, List<CenterOfMass> list)
    {
        rb.isKinematic = true;
        transform.parent = parent;
        list.Add(centerOfMass);
    }

    public void Unequip(List<CenterOfMass> list)
    {
        list.Remove(centerOfMass);
        transform.parent = null;
        rb.isKinematic = false;
    }
}
