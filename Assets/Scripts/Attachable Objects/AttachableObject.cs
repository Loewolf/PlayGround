using UnityEngine;

public class AttachableObject : CenterOfMass
{
    public Rigidbody rigidbodyHandler;
    public GameObject antiSelfIntersectionBox;

    public void LeaveHandler()
    {
        rigidbodyHandler.useGravity = false;
        rigidbodyHandler.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void ReturnToHandler()
    {
        rigidbodyHandler.transform.position = transform.position;
        rigidbodyHandler.transform.rotation = transform.rotation;
        transform.parent = rigidbodyHandler.gameObject.transform;
        rigidbodyHandler.useGravity = true;
        rigidbodyHandler.constraints = RigidbodyConstraints.None;
    }

    private void OnDestroy()
    {
        if (rigidbodyHandler) Destroy(rigidbodyHandler.gameObject);
    }
}
