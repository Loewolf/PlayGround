using UnityEngine;
[AddComponentMenu("_Center Of Mass/Local")]
public class CenterOfMass : MonoBehaviour
{
    public Vector3 centerOfMass;
    public float mass;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.TransformPoint(centerOfMass), 0.1f);
    }
}
