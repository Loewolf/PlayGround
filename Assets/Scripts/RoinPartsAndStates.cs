using UnityEngine;

public class RoinPartsAndStates : MonoBehaviour
{
    [Header("Части РОИНа")]
    public Transform _body;
    public Transform _arm0,
                     _arm1,
                     _arm2,
                     _arm3,
                     _arm4,
                     _arm5,
                     _arm6,
                     _link1,
                     _link2,
                     _link3,
                     _link4,
                     _link5,
                     _link6,
                     _cylinder1,
                     _cylinder2,
                     _cylinder3,
                     _cylinder5,
                     _cylinder7,
                     _piston1,
                     _piston2,
                     _piston3,
                     _piston5,
                     _piston7,
                     _fixed3,
                     _fixed5,
                     _fixed7,
                     _clutch5,
                     _clutch7,
                     _tong,

                     _footFR,
                     _cylinderFR,
                     _pistonFR,
                     _footFL,
                     _cylinderFL,
                     _pistonFL,
                     _footBR,
                     _cylinderBR,
                     _pistonBR,
                     _footBL,
                     _cylinderBL,
                     _pistonBL,

                     _pitcher,
                     _cylinderRoller,
                     _roller,
                     _yawer,
                     _grip,
                     _pistonRoller;
    protected Transform body;
    protected Transform arm0,
                     arm1,
                     arm2,
                     arm3,
                     arm4,
                     arm5,
                     arm6,
                     link1,
                     link2,
                     link3,
                     link4,
                     link5,
                     link6,
                     cylinder1,
                     cylinder2,
                     cylinder3,
                     cylinder5,
                     cylinder7,
                     piston1,
                     piston2,
                     piston3,
                     piston5,
                     piston7,
                     fixed3,
                     fixed5,
                     fixed7,
                     clutch5,
                     clutch7,
                     tong,

                     footFR,
                     cylinderFR,
                     pistonFR,
                     footFL,
                     cylinderFL,
                     pistonFL,
                     footBR,
                     cylinderBR,
                     pistonBR,
                     footBL,
                     cylinderBL,
                     pistonBL,

                     pitcher,
                     cylinderRoller,
                     roller,
                     yawer,
                     grip,
                     pistonRoller;

    [Header("Настройки скорости")]
    public float _speed = 2.5f;
    public float _rotationSpeed = 0.5f,
                 _elongationSpeed = 0.5f;
    protected float currentSpeed, defaultSpeed,
                  currentSpeedRotation, defaultSpeedRotation,
                  currentSpeedElongation, defaultSpeedElongation;

    protected const float PI = Mathf.PI;

    protected bool allowRotation;
    protected bool allowMove;

    protected void MatchProperties()
    {
        defaultSpeed = _speed * Time.fixedDeltaTime;
        defaultSpeedRotation = _rotationSpeed * Time.fixedDeltaTime;
        defaultSpeedElongation = _elongationSpeed * Time.fixedDeltaTime;

        currentSpeedElongation = defaultSpeedElongation; // Не изменяется

        allowRotation = false;
        allowMove = true;

        body = _body;
        arm0 = _arm0;
        arm1 = _arm1;
        arm2 = _arm2;
        arm3 = _arm3;
        arm4 = _arm4;
        arm5 = _arm5;
        arm6 = _arm6;
        link1 = _link1;
        link2 = _link2;
        link3 = _link3;
        link4 = _link4;
        link5 = _link5;
        link6 = _link6;
        cylinder1 = _cylinder1;
        cylinder2 = _cylinder2;
        cylinder3 = _cylinder3;
        cylinder5 = _cylinder5;
        cylinder7 = _cylinder7;
        piston1 = _piston1;
        piston2 = _piston2;
        piston3 = _piston3;
        piston5 = _piston5;
        piston7 = _piston7;
        fixed3 = _fixed3;
        fixed5 = _fixed5;
        fixed7 = _fixed7;
        clutch5 = _clutch5;
        clutch7 = _clutch7;
        tong = _tong;

        footFR = _footFR;
        cylinderFR = _cylinderFR;
        pistonFR = _pistonFR;
        footFL = _footFL;
        cylinderFL = _cylinderFL;
        pistonFL = _pistonFL;
        footBR = _footBR;
        cylinderBR = _cylinderBR;
        pistonBR = _pistonBR;
        footBL = _footBL;
        cylinderBL = _cylinderBL;
        pistonBL = _pistonBL;

        pitcher = _pitcher;
        cylinderRoller = _cylinderRoller;
        roller = _roller;
        yawer = _yawer;
        grip = _grip;
        pistonRoller = _pistonRoller;
}
    public void WriteValuesToStatesKit(RoinStatesKit kit)
    {
        kit.speed = defaultSpeed;
        kit.speedRotation = defaultSpeedRotation;
        kit.speedElongation = defaultSpeedElongation;

        kit.allowMove = allowMove;
        kit.allowRotation = allowRotation;

        kit.body_position = body.transform.localPosition;
        kit.arm0_position = arm0.transform.localPosition;
        kit.arm1_position = arm1.transform.localPosition;
        kit.arm2_position = arm2.transform.localPosition;
        kit.arm3_position = arm3.transform.localPosition;
        kit.arm4_position = arm4.transform.localPosition;
        kit.arm5_position = arm5.transform.localPosition;
        kit.arm6_position = arm6.transform.localPosition;
        kit.link1_position = link1.transform.localPosition;
        kit.link2_position = link2.transform.localPosition;
        kit.link3_position = link3.transform.localPosition;
        kit.link4_position = link4.transform.localPosition;
        kit.link5_position = link5.transform.localPosition;
        kit.link6_position = link6.transform.localPosition;
        kit.cylinder1_position = cylinder1.transform.localPosition;
        kit.cylinder2_position = cylinder2.transform.localPosition;
        kit.cylinder3_position = cylinder3.transform.localPosition;
        kit.cylinder5_position = cylinder5.transform.localPosition;
        kit.cylinder7_position = cylinder7.transform.localPosition;
        kit.piston1_position = piston1.transform.localPosition;
        kit.piston2_position = piston2.transform.localPosition;
        kit.piston3_position = piston3.transform.localPosition;
        kit.piston5_position = piston5.transform.localPosition;
        kit.piston7_position = piston7.transform.localPosition;
        kit.fixed3_position = fixed3.transform.localPosition;
        kit.fixed5_position = fixed5.transform.localPosition;
        kit.fixed7_position = fixed7.transform.localPosition;
        kit.clutch5_position = clutch5.transform.localPosition;
        kit.clutch7_position = clutch7.transform.localPosition;
        kit.tong_position = tong.transform.localPosition;

        kit.footFR_position = footFR.transform.localPosition;
        kit.cylinderFR_position = cylinderFR.transform.localPosition;
        kit.pistonFR_position = pistonFR.transform.localPosition;
        kit.footFL_position = footFL.transform.localPosition;
        kit.cylinderFL_position = cylinderFL.transform.localPosition;
        kit.pistonFL_position = pistonFL.transform.localPosition;
        kit.footBR_position = footBR.transform.localPosition;
        kit.cylinderBR_position = cylinderBR.transform.localPosition;
        kit.pistonBR_position = pistonBR.transform.localPosition;
        kit.footBL_position = footBL.transform.localPosition;
        kit.cylinderBL_position = cylinderBL.transform.localPosition;
        kit.pistonBL_position = pistonBL.transform.localPosition;

        kit.pitcher_position = pitcher.transform.localPosition;
        kit.cylinderRoller_position = cylinderRoller.transform.localPosition;
        kit.roller_position = roller.transform.localPosition;
        kit.yawer_position = yawer.transform.localPosition;
        kit.grip_position = grip.transform.localPosition;
        kit.pistonRoller_position = pistonRoller.transform.localPosition;

        // Rotation
        kit.body_rotation = body.transform.localRotation;
        kit.arm0_rotation = arm0.transform.localRotation;
        kit.arm1_rotation = arm1.transform.localRotation;
        kit.arm2_rotation = arm2.transform.localRotation;
        kit.arm3_rotation = arm3.transform.localRotation;
        kit.arm4_rotation = arm4.transform.localRotation;
        kit.arm5_rotation = arm5.transform.localRotation;
        kit.arm6_rotation = arm6.transform.localRotation;
        kit.link1_rotation = link1.transform.localRotation;
        kit.link2_rotation = link2.transform.localRotation;
        kit.link3_rotation = link3.transform.localRotation;
        kit.link4_rotation = link4.transform.localRotation;
        kit.link5_rotation = link5.transform.localRotation;
        kit.link6_rotation = link6.transform.localRotation;
        kit.cylinder1_rotation = cylinder1.transform.localRotation;
        kit.cylinder2_rotation = cylinder2.transform.localRotation;
        kit.cylinder3_rotation = cylinder3.transform.localRotation;
        kit.cylinder5_rotation = cylinder5.transform.localRotation;
        kit.cylinder7_rotation = cylinder7.transform.localRotation;
        kit.piston1_rotation = piston1.transform.localRotation;
        kit.piston2_rotation = piston2.transform.localRotation;
        kit.piston3_rotation = piston3.transform.localRotation;
        kit.piston5_rotation = piston5.transform.localRotation;
        kit.piston7_rotation = piston7.transform.localRotation;
        kit.fixed3_rotation = fixed3.transform.localRotation;
        kit.fixed5_rotation = fixed5.transform.localRotation;
        kit.fixed7_rotation = fixed7.transform.localRotation;
        kit.clutch5_rotation = clutch5.transform.localRotation;
        kit.clutch7_rotation = clutch7.transform.localRotation;
        kit.tong_rotation = tong.transform.localRotation;

        kit.footFR_rotation = footFR.transform.localRotation;
        kit.cylinderFR_rotation = cylinderFR.transform.localRotation;
        kit.pistonFR_rotation = pistonFR.transform.localRotation;
        kit.footFL_rotation = footFL.transform.localRotation;
        kit.cylinderFL_rotation = cylinderFL.transform.localRotation;
        kit.pistonFL_rotation = pistonFL.transform.localRotation;
        kit.footBR_rotation = footBR.transform.localRotation;
        kit.cylinderBR_rotation = cylinderBR.transform.localRotation;
        kit.pistonBR_rotation = pistonBR.transform.localRotation;
        kit.footBL_rotation = footBL.transform.localRotation;
        kit.cylinderBL_rotation = cylinderBL.transform.localRotation;
        kit.pistonBL_rotation = pistonBL.transform.localRotation;

        kit.pitcher_rotation = pitcher.transform.localRotation;
        kit.cylinderRoller_rotation = cylinderRoller.transform.localRotation;
        kit.roller_rotation = roller.transform.localRotation;
        kit.yawer_rotation = yawer.transform.localRotation;
        kit.grip_rotation = grip.transform.localRotation;
        kit.pistonRoller_rotation = pistonRoller.transform.localRotation;
    }

    public void SetValuesFromStatesKit(RoinStatesKit kit)
    {
        defaultSpeed = kit.speed;
        defaultSpeedRotation = kit.speedRotation;
        defaultSpeedElongation = kit.speedElongation;

        currentSpeedElongation = defaultSpeedElongation;

        allowMove = kit.allowMove;
        allowRotation = kit.allowRotation;

        body.transform.localPosition = kit.body_position;
        arm0.transform.localPosition = kit.arm0_position;
        arm1.transform.localPosition = kit.arm1_position;
        arm2.transform.localPosition = kit.arm2_position;
        arm3.transform.localPosition = kit.arm3_position;
        arm4.transform.localPosition = kit.arm4_position;
        arm5.transform.localPosition = kit.arm5_position;
        arm6.transform.localPosition = kit.arm6_position;
        link1.transform.localPosition = kit.link1_position;
        link2.transform.localPosition = kit.link2_position;
        link3.transform.localPosition = kit.link3_position;
        link4.transform.localPosition = kit.link4_position;
        link5.transform.localPosition = kit.link5_position;
        link6.transform.localPosition = kit.link6_position;
        cylinder1.transform.localPosition = kit.cylinder1_position;
        cylinder2.transform.localPosition = kit.cylinder2_position;
        cylinder3.transform.localPosition = kit.cylinder3_position;
        cylinder5.transform.localPosition = kit.cylinder5_position;
        cylinder7.transform.localPosition = kit.cylinder7_position;
        piston1.transform.localPosition = kit.piston1_position;
        piston2.transform.localPosition = kit.piston2_position;
        piston3.transform.localPosition = kit.piston3_position;
        piston5.transform.localPosition = kit.piston5_position;
        piston7.transform.localPosition = kit.piston7_position;
        fixed3.transform.localPosition = kit.fixed3_position;
        fixed5.transform.localPosition = kit.fixed5_position;
        fixed7.transform.localPosition = kit.fixed7_position;
        clutch5.transform.localPosition = kit.clutch5_position;
        clutch7.transform.localPosition = kit.clutch7_position;
        tong.transform.localPosition = kit.tong_position;

        footFR.transform.localPosition = kit.footFR_position;
        cylinderFR.transform.localPosition = kit.cylinderFR_position;
        pistonFR.transform.localPosition = kit.pistonFR_position;
        footFL.transform.localPosition = kit.footFL_position;
        cylinderFL.transform.localPosition = kit.cylinderFL_position;
        pistonFL.transform.localPosition = kit.pistonFL_position;
        footBR.transform.localPosition = kit.footBR_position;
        cylinderBR.transform.localPosition = kit.cylinderBR_position;
        pistonBR.transform.localPosition = kit.pistonBR_position;
        footBL.transform.localPosition = kit.footBL_position;
        cylinderBL.transform.localPosition = kit.cylinderBL_position;
        pistonBL.transform.localPosition = kit.pistonBL_position;

        pitcher.transform.localPosition = kit.pitcher_position;
        cylinderRoller.transform.localPosition = kit.cylinderRoller_position;
        roller.transform.localPosition = kit.roller_position;
        yawer.transform.localPosition = kit.yawer_position;
        grip.transform.localPosition = kit.grip_position;
        pistonRoller.transform.localPosition = kit.pistonRoller_position;

        // Rotation
        body.transform.localRotation = kit.body_rotation;
        arm0.transform.localRotation = kit.arm0_rotation;
        arm1.transform.localRotation = kit.arm1_rotation;
        arm2.transform.localRotation = kit.arm2_rotation;
        arm3.transform.localRotation = kit.arm3_rotation;
        arm4.transform.localRotation = kit.arm4_rotation;
        arm5.transform.localRotation = kit.arm5_rotation;
        arm6.transform.localRotation = kit.arm6_rotation;
        link1.transform.localRotation = kit.link1_rotation;
        link2.transform.localRotation = kit.link2_rotation;
        link3.transform.localRotation = kit.link3_rotation;
        link4.transform.localRotation = kit.link4_rotation;
        link5.transform.localRotation = kit.link5_rotation;
        link6.transform.localRotation = kit.link6_rotation;
        cylinder1.transform.localRotation = kit.cylinder1_rotation;
        cylinder2.transform.localRotation = kit.cylinder2_rotation;
        cylinder3.transform.localRotation = kit.cylinder3_rotation;
        cylinder5.transform.localRotation = kit.cylinder5_rotation;
        cylinder7.transform.localRotation = kit.cylinder7_rotation;
        piston1.transform.localRotation = kit.piston1_rotation;
        piston2.transform.localRotation = kit.piston2_rotation;
        piston3.transform.localRotation = kit.piston3_rotation;
        piston5.transform.localRotation = kit.piston5_rotation;
        piston7.transform.localRotation = kit.piston7_rotation;
        fixed3.transform.localRotation = kit.fixed3_rotation;
        fixed5.transform.localRotation = kit.fixed5_rotation;
        fixed7.transform.localRotation = kit.fixed7_rotation;
        clutch5.transform.localRotation = kit.clutch5_rotation;
        clutch7.transform.localRotation = kit.clutch7_rotation;
        tong.transform.localRotation = kit.tong_rotation;

        footFR.transform.localRotation = kit.footFR_rotation;
        cylinderFR.transform.localRotation = kit.cylinderFR_rotation;
        pistonFR.transform.localRotation = kit.pistonFR_rotation;
        footFL.transform.localRotation = kit.footFL_rotation;
        cylinderFL.transform.localRotation = kit.cylinderFL_rotation;
        pistonFL.transform.localRotation = kit.pistonFL_rotation;
        footBR.transform.localRotation = kit.footBR_rotation;
        cylinderBR.transform.localRotation = kit.cylinderBR_rotation;
        pistonBR.transform.localRotation = kit.pistonBR_rotation;
        footBL.transform.localRotation = kit.footBL_rotation;
        cylinderBL.transform.localRotation = kit.cylinderBL_rotation;
        pistonBL.transform.localRotation = kit.pistonBL_rotation;

        pitcher.transform.localRotation = kit.pitcher_rotation;
        cylinderRoller.transform.localRotation = kit.cylinderRoller_rotation;
        roller.transform.localRotation = kit.roller_rotation;
        yawer.transform.localRotation = kit.yawer_rotation;
        grip.transform.localRotation = kit.grip_rotation;
        pistonRoller.transform.localRotation = kit.pistonRoller_rotation;
    }
}
