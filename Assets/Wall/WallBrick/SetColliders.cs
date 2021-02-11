using System.Collections;
using UnityEngine;
[ExecuteInEditMode]
public class SetColliders : MonoBehaviour
{
    public bool setColliders;

    void Update()
    {
        if(setColliders)
        {
            foreach(Transform child in transform)
            {
                child.gameObject.AddComponent<BoxCollider>();
            }
        }
    }
}
