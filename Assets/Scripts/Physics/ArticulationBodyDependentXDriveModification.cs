using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ArticulationBody))]
public class ArticulationBodyDependentXDriveModification : ArticulationBodyXDriveModification
{
    public List<ArticulationBodyXDriveModification> independentModifications;
    [Min(0f)] public float targetDeviation = 1f;

    private bool lowerRule()
    {
        float lowerTarget = independentModifications[0].GetXDriveTarget();
        for (int i = 1; i < independentModifications.Count; ++i)
        {
            float newTarget = independentModifications[i].GetXDriveTarget();
            if (newTarget > lowerTarget) lowerTarget = newTarget;
        }
        return GetXDriveTarget() < lowerTarget + targetDeviation;
    }

    protected override void OnIncreaseAction()
    {
        if (lowerRule())
        {
            base.OnIncreaseAction();
        }
    }
}