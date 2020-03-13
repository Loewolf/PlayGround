using UnityEngine;

public class AttachableObject : CenterOfMass
{
    public Rigidbody rigidbodyTaker;
    public GameObject antiSelfIntersectionBox;

    public void LeaveTaker(Example ex)
    {
        rigidbodyTaker.useGravity = false;
        rigidbodyTaker.constraints = RigidbodyConstraints.FreezeAll;
        if (antiSelfIntersectionBox) ex.ChangeAntiIntersectionBox(antiSelfIntersectionBox);
    }

    public void ReturnToTaker(Example ex)
    {
        rigidbodyTaker.transform.position = transform.position;
        rigidbodyTaker.transform.rotation = transform.rotation;
        transform.parent = rigidbodyTaker.gameObject.transform;
        rigidbodyTaker.useGravity = true;
        rigidbodyTaker.constraints = RigidbodyConstraints.None;
        ex.SetDefaultAntiIntersectionBox();
    }
}
