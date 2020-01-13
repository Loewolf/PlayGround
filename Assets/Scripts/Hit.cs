using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public float Radius;
    public float Force;

    void Update()   
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Radius);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].GetComponent<Destr>())
            {
                hitColliders[i].GetComponent<Destr>().Dead();
            }
            if (hitColliders[i].CompareTag("CanBer"))
            {
                hitColliders[i].gameObject.AddComponent<Rigidbody>();
            }
        }

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }

}
