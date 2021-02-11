using UnityEngine;

public class RigidbodyAttachableObject : CenterOfMass
{
    public new Rigidbody rigidbody;

    protected virtual void Awake()
    {
        rigidbody.centerOfMass = centerOfMass;
        rigidbody.mass = mass;
    }

    public void LeaveHandler(Transform parent)
    {
        rigidbody.useGravity = false;
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        transform.parent = parent;
    }

    public void ReturnToHandler()
    {
        rigidbody.transform.position = transform.position;
        rigidbody.transform.rotation = transform.rotation;
        transform.parent = rigidbody.gameObject.transform;
        rigidbody.useGravity = true;
        rigidbody.constraints = RigidbodyConstraints.None;
    }

    private void OnDestroy()
    {
        if (rigidbody) Destroy(rigidbody.gameObject);
    }
}