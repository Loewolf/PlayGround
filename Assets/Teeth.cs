using UnityEngine;

public class Teeth : MonoBehaviour
{
    public Grab grab;
    public bool isLeft;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) return;
        RigidbodyCustomCenterOfMass cm = other.GetComponent<RigidbodyCustomCenterOfMass>();
        if (cm)
        {
            if (isLeft) grab.Left.Add(cm);
            else grab.Right.Add(cm);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) return;
        RigidbodyCustomCenterOfMass cm = other.GetComponent<RigidbodyCustomCenterOfMass>();
        if (cm)
        {
            if (isLeft) grab.Left.Remove(cm);
            else grab.Right.Remove(cm);
            grab.SetFree(cm);
        }
    }

}
