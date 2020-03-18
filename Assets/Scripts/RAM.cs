using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RAM
{
    public static float alpha_new, beta_new, gamma_new, phi_new, psi_new1, psi_new2, theta_new;
    const float PI = Mathf.PI;

    // Простой поворот стрелы
    public static void RotateArmEasy(float one, ref GameObject arm, ref GameObject cylinder, ref GameObject piston,
       ref float alpha, ref float beta, ref float gamma, float l, float d, float speedRotation)
    {
        float delta_alpha = speedRotation * one;
        alpha_new = alpha + delta_alpha;
        arm.transform.RotateAround(arm.transform.position, arm.transform.right, delta_alpha * Mathf.Rad2Deg);

        float h = OtherMath.DistanceThCosine(l, d, alpha_new);
        beta_new = OtherMath.CalcBeta(l, d, h, alpha_new);
        float delta_beta = beta_new - beta;
        cylinder.transform.RotateAround(cylinder.transform.position, cylinder.transform.right, -delta_beta * Mathf.Rad2Deg);

        gamma_new = PI - alpha_new - beta_new;
        float delta_gamma = gamma_new - gamma;
        piston.transform.RotateAround(piston.transform.position, piston.transform.right, delta_gamma * Mathf.Rad2Deg);

        alpha = alpha_new;
        beta = beta_new;
        gamma = gamma_new;
    }

    // Поворот основания и рыскание
    public static void RotateBase_Yaw(float one, ref GameObject arm, ref float alpha, float speedRotation)
    {
        float delta_alpha = speedRotation * one;
        arm.transform.RotateAround(arm.transform.position, arm.transform.right, delta_alpha * Mathf.Rad2Deg);
        alpha += delta_alpha;
    }

    // Излом третьей стрелы и выравнивание четвертой
    public static void Fracture(float one, ref GameObject arm3, ref GameObject arm4, ref GameObject cylinder, ref GameObject piston,
       ref GameObject fixed3, ref float alpha, ref float beta, ref float gamma, float l, float d, float speedRotation)
    {
        float delta_alpha = speedRotation * one;
        alpha_new = alpha - delta_alpha;

        arm3.transform.RotateAround(arm3.transform.position, arm3.transform.right, delta_alpha * Mathf.Rad2Deg);
        arm4.transform.RotateAround(arm4.transform.position, arm4.transform.right, -delta_alpha * Mathf.Rad2Deg);
        fixed3.transform.RotateAround(fixed3.transform.position, fixed3.transform.right, delta_alpha * Mathf.Rad2Deg);

        float h = OtherMath.DistanceThCosine(l, d, alpha_new);
        beta_new = OtherMath.CalcBeta(l, d, h, alpha_new);
        float delta_beta = beta_new - beta;
        cylinder.transform.RotateAround(cylinder.transform.position, cylinder.transform.right, delta_beta * Mathf.Rad2Deg);

        gamma_new = PI - alpha_new - beta_new;
        float delta_gamma = gamma_new - gamma;
        piston.transform.RotateAround(piston.transform.position, piston.transform.right, -delta_gamma * Mathf.Rad2Deg);

        alpha = alpha_new;
        beta = beta_new;
        gamma = gamma_new;
    }

    // Поворот пятой стрелы относительно наподвижной четвертой и тангаж
    public static void RotateArmHard_Pitch(float one, ref GameObject arm, ref GameObject cylinder, ref GameObject piston, ref GameObject clutch,
       ref GameObject fixed5, ref float alpha, ref float beta, ref float gamma, ref float theta, ref float phi, ref float psi1,
       ref float psi2, float OA, float OB, float OC, float BD, float CD, float speedRotation)
    {
        float delta_alpha = speedRotation * one;
        alpha_new = alpha - delta_alpha;
        fixed5.transform.RotateAround(fixed5.transform.position, fixed5.transform.right, delta_alpha * Mathf.Rad2Deg);

        float h = OtherMath.DistanceThCosine(OA, OB, alpha_new);
        beta_new = OtherMath.CalcBeta(OA, OB, h, alpha_new);
        float delta_beta = beta_new - beta;
        cylinder.transform.RotateAround(cylinder.transform.position, cylinder.transform.right, delta_beta * Mathf.Rad2Deg);

        gamma_new = PI - alpha_new - beta_new;
        float delta_gamma = gamma_new - gamma;
        piston.transform.RotateAround(piston.transform.position, piston.transform.right, -delta_gamma * Mathf.Rad2Deg);

        float delta_phi = speedRotation * one;
        phi_new = phi + delta_phi;

        h = OtherMath.DistanceThCosine(OC, OB, phi_new);
        psi_new1 = Mathf.Acos((OC * OC + h * h - OB * OB) / (2 * OC * h));
        psi_new2 = Mathf.Acos((CD * CD + h * h - BD * BD) / (2 * CD * h));
        float delta_psi = one * (Mathf.Abs(psi_new1 - psi1) + Mathf.Abs(psi_new2 - psi2));
        arm.transform.RotateAround(arm.transform.position, arm.transform.right, delta_psi * Mathf.Rad2Deg);

        theta_new = Mathf.Acos((BD * BD + CD * CD - h * h) / (2 * BD * CD));
        float delta_theta = theta_new - theta;
        clutch.transform.RotateAround(clutch.transform.position, clutch.transform.right, -delta_theta * Mathf.Rad2Deg);

        alpha = alpha_new;
        beta = beta_new;
        gamma = gamma_new;
        psi1 = psi_new1;
        psi2 = psi_new2;
        phi = phi_new;
        theta = theta_new;
    }

    // Выдвижение шестой стрелы
    public static void Elongation(float one, ref GameObject arm, ref float l, float speedElongation)
    {
        arm.transform.position -= (one * arm.transform.up * speedElongation);
        l += (one * speedElongation);
    }

    // Крен НПУ
    public static void RotateRoll(float one, ref GameObject arm, ref GameObject cylinder, ref GameObject piston,
       ref float alpha, ref float beta, ref float gamma, float l, float d, float speedRotation)
    {
        float delta_alpha = speedRotation * one;
        alpha_new = alpha - delta_alpha;
        arm.transform.RotateAround(arm.transform.position, arm.transform.right, delta_alpha * Mathf.Rad2Deg);

        float h = OtherMath.DistanceThCosine(l, d, alpha_new);
        beta_new = OtherMath.CalcBeta(l, d, h, alpha_new);
        float delta_beta = beta_new - beta;
        cylinder.transform.RotateAround(cylinder.transform.position, cylinder.transform.right, delta_beta * Mathf.Rad2Deg);

        gamma_new = PI - alpha_new - beta_new;
        float delta_gamma = gamma_new - gamma;
        piston.transform.RotateAround(piston.transform.position, piston.transform.right, delta_gamma * Mathf.Rad2Deg);

        alpha = alpha_new;
        beta = beta_new;
        gamma = gamma_new;
    }

    // Движение лап
    public static void RaiseFoot(float one, ref GameObject foot, ref GameObject cylinder, ref GameObject piston,
        ref float alpha, ref float beta, ref float gamma, float l, float d, float speedRotation)
    {
        float delta_alpha = speedRotation * one;
        alpha_new = alpha + delta_alpha;
        foot.transform.RotateAround(foot.transform.position, foot.transform.right, delta_alpha * Mathf.Rad2Deg);
        float h = OtherMath.DistanceThCosine(l, d, alpha_new);

        beta_new = OtherMath.CalcBeta(l, d, h, alpha_new);
        float delta_beta = beta_new - beta;
        cylinder.transform.RotateAround(cylinder.transform.position, cylinder.transform.right, -delta_beta * Mathf.Rad2Deg);

        gamma_new = PI - alpha_new - beta_new;
        float delta_gamma = gamma_new - gamma;
        piston.transform.RotateAround(piston.transform.position, piston.transform.right, delta_gamma * Mathf.Rad2Deg);
        alpha = alpha_new;
        beta = beta_new;
        gamma = gamma_new;
    }
}
