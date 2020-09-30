using UnityEngine;

public class ReachablePointNoColliderCheck : ReachablePoint
// Используется в уровне с описанием фигуры стрелой
{
    public override void ResetReached()
    {
        objectsAtPoint = 0;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (CheckForMatches(other))
        {
            objectsAtPoint++;
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
    }
}
