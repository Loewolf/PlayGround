using UnityEngine;

public class SpiralPoints : MonoBehaviour
{
    [Min(0f)] public float lowerRadius;
    [Min(0f)] public float upperRadius;
    [Space(15)]
    [Range(0f, 360f)] public float lowerAngle;
    [Range(0f, 360f)] public float upperAngle;
    [Space(15)]
    [Min(0)] public int stepAtStart;

    private const float VOGEL = 16.4495926918f;
    private const float DOUBLE_PI = Mathf.PI * 2f;

    private int pointsCount;
    private float angleRadAddition; // Случайное число от 0 до 2*PI
    private float[] anglesRad;
    private int[] steps;
    private float vogelMultiplier;

    private float lowerAngleRad;
    private float upperAngleRelativeToLowerRad;

    private void SetBorders()
    {
        lowerAngleRad = lowerAngle * Mathf.Deg2Rad;
        upperAngleRelativeToLowerRad = upperAngle - lowerAngle;
        if (upperAngleRelativeToLowerRad < 0) upperAngleRelativeToLowerRad += 360f;
        upperAngleRelativeToLowerRad *= Mathf.Deg2Rad;
    }

    public void CreateSequence(in int count)
    {
        SetBorders();
        pointsCount = count;
        anglesRad = new float[pointsCount];
        steps = new int[pointsCount];
        angleRadAddition = Random.Range(0f, DOUBLE_PI);
        if (pointsCount > 0)
        {
            steps[0] = stepAtStart;
            SetVogelAngle(0);
            for (int i = 1; i < pointsCount; ++i)
            {
                steps[i] = steps[i - 1] + 1;
                SetVogelAngle(i);
            }
            vogelMultiplier = (upperRadius - lowerRadius) / Mathf.Sqrt(steps[pointsCount - 1]);
        }
    }

    public Vector3 GetWorldPositionOfPoint(in int i)
    {
        if (i > -1 && i < pointsCount)
            return transform.position + PolarCoordinateSystem.PolarToCartesianXZ(anglesRad[i],
                                        lowerRadius + vogelMultiplier * Mathf.Sqrt(steps[i]));
        else return Vector3.zero;
    }

    private bool OutsideBorders(float angleRad)
    {
        angleRad %= DOUBLE_PI;
        angleRad -= lowerAngleRad;
        if (angleRad < 0) angleRad += DOUBLE_PI;
        if (angleRad <= upperAngleRelativeToLowerRad) return false;
        else return true;
    }

    private void SetVogelAngle(in int i)
    {
        int steps = this.steps[i];
        float angleRad = VOGEL * steps + angleRadAddition;
        while (OutsideBorders(angleRad))
        {
            steps++;
            angleRad = VOGEL * steps + angleRadAddition;
        }
        anglesRad[i] = angleRad;
        this.steps[i] = steps;
    }

    private void OnDrawGizmosSelected()
    {
        SetBorders();
        Gizmos.color = Color.green;
        Vector3 previousPointLower = transform.position + PolarCoordinateSystem.PolarToCartesianXZ(lowerAngleRad, lowerRadius),
                previousPointUpper = transform.position + PolarCoordinateSystem.PolarToCartesianXZ(lowerAngleRad, upperRadius),
            currentPointLower, currentPointUpper;
        Gizmos.DrawLine(previousPointLower, previousPointUpper);

        int n = 32;
        float nInversed = upperAngleRelativeToLowerRad / n, currentAngle;

        for (int i = 1; i <= n; ++i)
        {
            currentAngle = lowerAngleRad + i * nInversed;
            currentPointLower = transform.position + PolarCoordinateSystem.PolarToCartesianXZ(currentAngle, lowerRadius);
            currentPointUpper = transform.position + PolarCoordinateSystem.PolarToCartesianXZ(currentAngle, upperRadius);
            Gizmos.DrawLine(previousPointLower, currentPointLower);
            Gizmos.DrawLine(previousPointUpper, currentPointUpper);
            previousPointLower = currentPointLower;
            previousPointUpper = currentPointUpper;
        }
        Gizmos.DrawLine(previousPointLower, previousPointUpper);
    }
}
