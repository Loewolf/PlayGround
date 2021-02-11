using System.Collections.Generic;
using UnityEngine;

public class RigidbodyGrabTrigger : MonoBehaviour
{
    public RigidbodyGrab grab;
    public bool isLeft;
    private HashSet<RigidbodyAttachableObject> attachableObjects;

    private void Start()
    {
        attachableObjects = isLeft ? grab.leftAttachableObjectSet : grab.rightAttachableObjectSet;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!(other.gameObject.CompareTag("Player")))
        {
            RigidbodyAttachableObject attachableObject = other.GetComponent<RigidbodyAttachableObject>();
            if (attachableObject)
            {
                attachableObjects.Add(attachableObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!(other.gameObject.CompareTag("Player")))
        {
            RigidbodyAttachableObject attachableObject = other.GetComponent<RigidbodyAttachableObject>();
            if (attachableObject)
            {
                attachableObjects.Remove(attachableObject);
            }
        }
    }
}
