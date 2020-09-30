using System.Collections.Generic;
using UnityEngine;

public class CircularPath : CircleSegmentBorders
{
    [Min(0f)] public float lowerRadius;
    [Min(0f)] public float upperRadius;
    public Vector2 heightRange;
    public float yellowZoneHeightAddition;
    private List<float> anglesRad;
    private int count;
    [HideInInspector] public Vector3[] positions;
    [HideInInspector] public float[] progressAtPoint;

    private void Awake()
    {
        anglesRad = new List<float>();
    }

    public int SetPositions(int expectedSegmentsCount)
    {
        if (expectedSegmentsCount < 1) return 0;
        SetBorders();
        anglesRad.Clear();
        float step = DOUBLE_PI / expectedSegmentsCount, halfStep = step * 0.5f, threeSecondStep = step * 1.5f,
              angle = 0f, angleAddition = Random.Range(0f, DOUBLE_PI), direction = Mathf.Sign(Random.Range(-1f, 1f));
        angle = 0;
        anglesRad.Add(angle);
        while (DOUBLE_PI - angle > threeSecondStep)
        {
            angle += Random.Range(halfStep, threeSecondStep);
            anglesRad.Add(angle);
        }
        count = anglesRad.Count;
        positions = new Vector3[count];
        progressAtPoint = new float[count];

        positions[0] = transform.position + PolarCoordinateSystem.PolarToCartesianXZ(angleAddition, Random.Range(lowerRadius, upperRadius));
        if (OutsideBorders(angleAddition)) positions[0] += Vector3.up * Random.Range(heightRange.x, heightRange.y);
        else positions[0] += Vector3.up * (yellowZoneHeightAddition + Random.Range(heightRange.x, heightRange.y));

        float pathLength = 0;

        for (int i = 1; i < count; ++i)
        {
            angle = angleAddition + direction * anglesRad[i];
            positions[i] = transform.position + PolarCoordinateSystem.PolarToCartesianXZ(angle, Random.Range(lowerRadius, upperRadius));
            if (OutsideBorders(angle)) positions[i] += Vector3.up * Random.Range(heightRange.x, heightRange.y);
            else positions[i] += Vector3.up * (yellowZoneHeightAddition + Random.Range(heightRange.x, heightRange.y));
            pathLength += Vector3.Magnitude(positions[i] - positions[i - 1]);
            progressAtPoint[i - 1] = pathLength;
        }
        pathLength += Vector3.Magnitude(positions[0] - positions[count - 1]);
        progressAtPoint[count - 1] = pathLength;
        for (int i = 0; i < count; ++i)
        {
            progressAtPoint[i] /= pathLength;
        }
        return count;
    }

    public void GetPropertiesByIndex(int index, ref Vector3 position, ref float progress)
    {
        position = positions[index % count];
        progress = progressAtPoint[(index - 1) % count];
    }

    private void OnDrawGizmosSelected()
    {
        SetBorders();
        Gizmos.color = Color.yellow;
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

        Gizmos.color = Color.green;
        nInversed = (DOUBLE_PI - upperAngleRelativeToLowerRad) / n;
        float upperAngleRad = lowerAngleRad + upperAngleRelativeToLowerRad;
        for (int i = 1; i <= n; ++i)
        {
            currentAngle = upperAngleRad + i * nInversed;
            currentPointLower = transform.position + PolarCoordinateSystem.PolarToCartesianXZ(currentAngle, lowerRadius);
            currentPointUpper = transform.position + PolarCoordinateSystem.PolarToCartesianXZ(currentAngle, upperRadius);
            Gizmos.DrawLine(previousPointLower, currentPointLower);
            Gizmos.DrawLine(previousPointUpper, currentPointUpper);
            previousPointLower = currentPointLower;
            previousPointUpper = currentPointUpper;
        }
    }
}
