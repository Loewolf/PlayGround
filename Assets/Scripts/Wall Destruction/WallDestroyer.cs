using UnityEngine;

public class WallDestroyer : MonoBehaviour
{
    public float radius;
    public float force;

    private const string targetTag = "Wall Fragment";

    private void FixedUpdate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            hitColliders[i].GetComponent<BrickDestruction>()?.Dead();
            if (hitColliders[i].CompareTag(targetTag))
            {                
                hitColliders[i].gameObject.AddComponent<Rigidbody>().AddExplosionForce(force, transform.position, radius, 3.0F);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
