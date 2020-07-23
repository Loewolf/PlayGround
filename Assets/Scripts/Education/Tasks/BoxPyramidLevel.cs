using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class BoxPyramidLevel : MonoBehaviour
{
    public Vector3 globalScale;
    public int requiredBoxAmount; // Количество коробок, которое должно быть получено на заданном уровне
    private int currentBoxAmount = 0;
    private List<Collider> colliders;

    private void Awake()
    {
        colliders = new List<Collider>();
    }

    private void OnEnable()
    {
        currentBoxAmount = 0;
        colliders.Clear();
    }

    public int GetBoxTaskResult()
    {
        if (currentBoxAmount >= requiredBoxAmount)
            return 1;
        else
            return 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Box"&&!colliders.Contains(other))
        {
            colliders.Add(other);
            currentBoxAmount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Box" && colliders.Contains(other))
        {
            colliders.Remove(other);
            currentBoxAmount--;
        }
    }

    private void OnValidate()
    {
        transform.localScale = new Vector3(globalScale.x / transform.parent.transform.localScale.x, globalScale.y / transform.parent.transform.localScale.y, globalScale.z / transform.parent.transform.localScale.z);
    }
}
