using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class ReachablePoint : MonoBehaviour
{
    public string targetTag = "Player";
    public GameObject targetObject = null;

    protected int objectsAtPoint = 0;
    protected delegate bool CheckForMatchesDelegate(in Collider other);
    protected CheckForMatchesDelegate CheckForMatches;
    protected List<Collider> colliders;

    private void Awake()
    {
        colliders = new List<Collider>();
        if (targetObject) CheckForMatches = CheckGameObject;
        else CheckForMatches = CheckTag;
    }

    private void OnEnable()
    {
        ResetReached();
    }

    private bool CheckTag(in Collider other)
    {
        return other.tag == targetTag;
    }

    private bool CheckGameObject(in Collider other)
    {
        return other.gameObject == targetObject;
    }

    public bool IsReached()
    {
        return objectsAtPoint > 0;
    }

    public virtual void ResetReached()
    {
        objectsAtPoint = 0;
        if (colliders != null) colliders.Clear();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (CheckForMatches(other) && !colliders.Contains(other))
        {
            objectsAtPoint++;
            colliders.Add(other);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (CheckForMatches(other) && colliders.Contains(other))
        {
            objectsAtPoint--;
            colliders.Remove(other);
        }
    }
}
