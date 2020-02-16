using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("_Center Of Mass/General")]
[RequireComponent(typeof(Rigidbody))]
public class GeneralCenterOfMass : MonoBehaviour
{
    public List<CenterOfMass> list;
    public Vector3 centerOfMass;
    private Rigidbody r;
    private float totalMass;
    public bool _fixed;

    void Start()
    {
        r = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!_fixed)
        {
            RecalculateGeneralCenterOfMass();
        }
        r.centerOfMass = centerOfMass;
    }

    private void RecalculateGeneralCenterOfMass()
    {
        Vector3 xm = Vector3.zero;
        float m = 0;
        foreach (CenterOfMass cm in list)
        {
            xm += cm.transform.TransformPoint(cm.centerOfMass) * cm.mass;
            m += cm.mass;
        }
        centerOfMass = transform.InverseTransformPoint(xm / m);
        totalMass = m;
    }

    public void FindAllChildrenWithCenterOfMass(Transform localTransform)
    {
        foreach (Transform child in localTransform)
        {
            if (child.GetComponent<CenterOfMass>())
            {
                foreach (CenterOfMass center in child.GetComponents<CenterOfMass>())
                list.Add(center);
            }
            if (child.childCount > 0) FindAllChildrenWithCenterOfMass(child);
        }
    }

    public void CalculateCenterOfMass()
    {
        list.Clear();
        FindAllChildrenWithCenterOfMass(transform);
        RecalculateGeneralCenterOfMass();
        r = GetComponent<Rigidbody>();
        r.centerOfMass = centerOfMass;
        r.mass = totalMass;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.TransformPoint(centerOfMass), 0.15f);
    }
}
