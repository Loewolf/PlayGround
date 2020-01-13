using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destr : MonoBehaviour
{
    public Transform destroyed;
    public void Dead()
    {
        Instantiate(destroyed, transform.position + new Vector3(0,-2,0), Quaternion.Euler(0, 0, 0));
        Destroy(gameObject);
    }

}
