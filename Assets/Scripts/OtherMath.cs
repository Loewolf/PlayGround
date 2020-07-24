using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OtherMath
{
    // Расстояние между точками в параллельных плоскостях
    public static float DistanceYZ(GameObject A, GameObject B)
    {
        float Y = A.transform.position.y - B.transform.position.y;
        float Z = A.transform.position.z - B.transform.position.z;
        float dist = Mathf.Sqrt(Y * Y + Z * Z);
        return dist;
    }

    // Расстояние между точками в параллельных плоскостях
    public static float DistanceXY(GameObject A, GameObject B)
    {
        float X = A.transform.position.x - B.transform.position.x;
        float Y = A.transform.position.y - B.transform.position.y;
        float dist = Mathf.Sqrt(X * X + Y * Y);
        return dist;
    }

    // Расстояние между точками в пространстве
    public static float DistanceXYZ(GameObject A, GameObject B)
    {
        float X = A.transform.position.x - B.transform.position.x;
        float Y = A.transform.position.y - B.transform.position.y;
        float Z = A.transform.position.z - B.transform.position.z;
        float dist = Mathf.Sqrt(X * X + Y * Y + Z * Z);
        return dist;
    }

    // Расстояние между точками отнсительно родителя
    public static float DistanceLocalYZ(GameObject A, GameObject B)
    {
        float Y = A.transform.localPosition.y - B.transform.localPosition.y;
        float Z = A.transform.localPosition.z - B.transform.localPosition.z;
        float dist = Mathf.Sqrt(Y * Y + Z * Z);
        return dist;
    }

    // Вычисление угла Бета по теореме косинусов
    public static float CalcBeta(float l, float d, float h, float alpha)
    {
        float cos = (l * l + h * h - d * d) / (2 * h * l);
        float sin = Mathf.Sin(alpha) / h * d;
        float result = Mathf.Atan2(sin, cos);
        return result;
    }

    // Вычисление расстояния между осями соосных цилиндра и поршня по теореме косинусов
    public static float DistanceThCosine(float l, float d, float alpha)
    {
        float result = Mathf.Sqrt(l * l + d * d - 2 * d * l * Mathf.Cos(alpha));
        return result;
    }

    public static float SquareRootOfOneMinusSquareSrc(float src)
    {
        return Mathf.Sqrt(1 - src * src);
    }

    public static float CosOfAngleBetweenTwoVectors(Vector3 v1, Vector3 v2)
    {
        return (v1.x * v2.x + v1.y * v2.y + v1.z * v2.z) / (v1.magnitude * v2.magnitude);
    }
}

