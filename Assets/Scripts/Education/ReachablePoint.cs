using UnityEngine;
[RequireComponent(typeof(Collider))]
public class ReachablePoint : MonoBehaviour
{
    public string targetTag = "Player";
    protected bool reached = false;

    public void ResetReached()
    {
        reached = false;
    }

    public bool IsReached()
    {
        return reached;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == targetTag)
        {
            reached = true;
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.tag == targetTag)
        {
            reached = false;
        }
    }
}
