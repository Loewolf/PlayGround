using UnityEngine;
[RequireComponent(typeof(Rigidbody), typeof(CenterOfMass))]
[AddComponentMenu("_Center Of Mass/Rigidbody with custom Center Of Mass")]
public class RigidbodyCustomCenterOfMass : MonoBehaviour
{
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public CenterOfMass cm;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cm = GetComponent<CenterOfMass>();
        rb.centerOfMass = cm.centerOfMass;
        rb.mass = cm.mass;
    }
}
