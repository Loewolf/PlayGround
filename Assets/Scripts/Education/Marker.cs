using UnityEngine;

public class Marker : MonoBehaviour
{
    public bool lookAtCamera = true;
    public Collider target;
    public Vector3 offset;

    [Header("Динамика стрелки-указателя")]
    public bool dynamicArrow = false;
    public Transform arrow;
    public float dynamicLength = 1f;
    public float speed = 1f;

    private float step = 0f;
    private float direction = 1f;

    public void SetTarget(Collider collider)
    {
        target = collider;
        SetPosition();
    }

    private void SetPosition()
    {
        transform.position = target.bounds.center + new Vector3(0, target.bounds.extents.y, 0) + offset; 
    }

    private void DynamicArrow()
    {
        step += direction * Time.deltaTime * speed;
        if (step > dynamicLength)
        {
            direction = -1f;
            step = 2f * dynamicLength - step;
        }
        else if (step < 0)
        {
            direction = 1f;
            step *= -1f;
        }
        arrow.localPosition = new Vector3(0, step, 0);
    }

    private void Update()
    {
        if (target) SetPosition();
        if (lookAtCamera) transform.LookAt(Camera.main.transform);
        if (dynamicArrow) DynamicArrow();
    }
}
