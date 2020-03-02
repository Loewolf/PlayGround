using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public float radius;
    public float force;

    void Update()   
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].GetComponent<Destr>())
            {
                hitColliders[i].GetComponent<Destr>().Dead();
            }
            if (hitColliders[i].CompareTag("CanBer"))
            {
                hitColliders[i].gameObject.AddComponent<Rigidbody>();
                 Rigidbody rb = hitColliders[i].GetComponent<Rigidbody>();
                rb.AddExplosionForce(force, transform.position, radius, 3.0F);
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
