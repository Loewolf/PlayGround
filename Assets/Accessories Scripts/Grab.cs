using UnityEngine;
[AddComponentMenu("_Accessory/Grab")]
public class Grab : Accessory
{
    public GameObject leftClaw;
    public GameObject rightClaw;
    public float speed;
    public float topRotationBorderDegrees;
    public float lowRotationBorderDegrees;
    private float rot; // by Y axis

    public override void FirstAction()
    {
        rot += Time.deltaTime*speed;
        RotateClaws();
    }

    public override void SecondAction()
    {
        rot -= Time.deltaTime * speed;
        RotateClaws();
    }

    private void RotateClaws()
    {
        if (rot > topRotationBorderDegrees) rot = topRotationBorderDegrees;
        else if (rot < lowRotationBorderDegrees) rot = lowRotationBorderDegrees;
        leftClaw.transform.localRotation = Quaternion.Euler(0, rot, 0);
        rightClaw.transform.localRotation = Quaternion.Euler(0, -rot, 0);
    }
}
