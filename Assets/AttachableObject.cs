using UnityEngine;

public class AttachableObject : CenterOfMass
{
    public Rigidbody rigidbodyTaker;

    public void LeaveTaker()
    {
        rigidbodyTaker.useGravity = false;
        rigidbodyTaker.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void ReturnToTaker()
    {
        rigidbodyTaker.transform.position = transform.position;
        rigidbodyTaker.transform.rotation = transform.rotation;
        transform.parent = rigidbodyTaker.gameObject.transform;
        rigidbodyTaker.useGravity = true;
        rigidbodyTaker.constraints = RigidbodyConstraints.None;
    }
}
