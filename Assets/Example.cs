using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    public GameObject player;//сама платформа

    protected GameObject body,
                        cameras,
                        arm0,
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
                        link7,
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

    public float speed,
                 speedRotation,
                 speedElongation;

    public float PI = Mathf.PI;

    public bool allowRotation;
    public bool allowMove;


    public float hitFootMin, //для лап
                 hitFootMax,
                 hit1Min,
                 hit1Max,
                 hit2Min,
                 hit2Max,
                 hitFractureMin,
                 hitFractureMax,
                 hit5Min,
                 hit5Max,
                 hit6Min,
                 hit6Max,
                 hitPitchMin,
                 hitPitchMax,
                 hitRollMin,
                 hitRollMax;

    // Длины сторон и углы для вычисления треугольников
    public float alpha0,
                    alpha1, beta1, gamma1, d1, l1,
                    alpha2, beta2, gamma2, d2, l2,
                    alpha3, beta3, gamma3, d3, l3,
                    alpha5, beta5, gamma5, phi5, psi51, psi52, theta5,
                    OA5, OB5, CD5, BD5, OC5, //Размеры, которые остаются постоянными
                    l6,
                    alphaPitch, beta7, gamma7, phi7, psi71, psi72, theta7,
                    OA7, OB7, CD7, BD7, OC7, //Размеры, которые остаются постоянными
                    alphaFR, betaFR, gammaFR, dFR, lFR,
                    alphaFL, betaFL, gammaFL, dFL, lFL,
                    alphaBR, betaBR, gammaBR, dBR, lBR,
                    alphaBL, betaBL, gammaBL, dBL, lBL,
                    alphaRoll, betaRoll, gammaRoll, lRoll, dRoll,
                    alphaYaw;

    // Use this for initialization
    void Start()
    {
        TieObjiects();

        speedRotation = 0.5f * Time.deltaTime;
        speed = 2.5f * Time.deltaTime;
        speedElongation = 0.5f * Time.deltaTime;

        // расчет сторон и углов треугольников
        SetSize();
        SetHits();

        allowRotation = false;
        allowMove = true;
    }

    // Обработка нажтия клавиш
    void FixedUpdate()
    {
        //Движение
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            if (allowMove)
                player.transform.position += body.transform.forward * speed;
        }

        //Движение
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if (allowMove)
                player.transform.position -= body.transform.forward * speed;
        }

        //Поворот
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (allowMove)
            {
                player.transform.RotateAround(body.transform.position, body.transform.up, -speedRotation * Mathf.Rad2Deg);
            }
        }

        //Поворот
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (allowMove)
            {
                player.transform.RotateAround(body.transform.position, body.transform.up, speedRotation * Mathf.Rad2Deg);
            }
        }

        //Поворот основания
        if (Input.GetKey(KeyCode.R))
        {
            if (allowRotation)
                RAM.RotateBase_Yaw(1, ref arm0, ref alpha0, speedRotation);
        }

        //Поворот основания
        if (Input.GetKey(KeyCode.T))
        {
            if (allowRotation)
                RAM.RotateBase_Yaw(-1, ref arm0, ref alpha0, speedRotation);
        }

        //Поворот первой стрелы с неподвижной базой
        if (Input.GetKey(KeyCode.U))
        {
            if (allowRotation)
            {
                if (alpha1 <= hit1Max - speedRotation)
                    RAM.RotateArmEasy(1, ref arm1, ref cylinder1, ref piston1,
                        ref alpha1, ref beta1, ref gamma1,
                        l1, d1, speedRotation);
                else if (alpha1 < hit1Max)
                    RAM.RotateArmEasy((hit1Max - alpha1) / speedRotation, ref arm1, ref cylinder1, ref piston1,
                        ref alpha1, ref beta1, ref gamma1,
                        l1, d1, speedRotation);
            }
        }

        //Поворот первой стрелы с неподвижной базой
        if (Input.GetKey(KeyCode.Y))
        {
            if (allowRotation)
            {
                if (alpha1 >= hit1Min + speedRotation)
                    RAM.RotateArmEasy(-1, ref arm1, ref cylinder1, ref piston1,
                        ref alpha1, ref beta1, ref gamma1,
                        l1, d1, speedRotation);
                else if (alpha1 > hit1Min)
                    RAM.RotateArmEasy((hit1Min - alpha1) / speedRotation, ref arm1, ref cylinder1, ref piston1,
                        ref alpha1, ref beta1, ref gamma1,
                        l1, d1, speedRotation);
            }
        }

        //Поворот второй стрелы с неподвижной первой
        if (Input.GetKey(KeyCode.O))
        {

            if (allowRotation)
            {
                if (alpha2 <= hit2Max - speedRotation)
                    RAM.RotateArmEasy(1, ref arm2, ref cylinder2, ref piston2,
                        ref alpha2, ref beta2, ref gamma2,
                        l2, d2, speedRotation);
                else if (alpha2 < hit2Max)
                    RAM.RotateArmEasy((hit2Max - alpha2) / speedRotation, ref arm2, ref cylinder2, ref piston2,
                        ref alpha2, ref beta2, ref gamma2,
                        l2, d2, speedRotation);
            }
        }

        //Поворот второй стрелы с неподвижной первой
        if (Input.GetKey(KeyCode.I))
        {
            if (allowRotation)
            {
                if (alpha2 >= hit2Min + speedRotation)
                    RAM.RotateArmEasy(-1, ref arm2, ref cylinder2, ref piston2,
                       ref alpha2, ref beta2, ref gamma2,
                       l2, d2, speedRotation);
                else if (alpha2 > hit2Min)
                    RAM.RotateArmEasy((hit2Min - alpha2) / speedRotation, ref arm2, ref cylinder2, ref piston2,
                       ref alpha2, ref beta2, ref gamma2,
                       l2, d2, speedRotation);
            }
        }

        //Излом
        if (Input.GetKey(KeyCode.F))
        {
            if (allowRotation)
            {
                if (alpha3 >= hitFractureMin + speedRotation)
                    RAM.Fracture(1, ref arm3, ref arm4, ref cylinder3, ref piston3, ref fixed3,
                        ref alpha3, ref beta3, ref gamma3,
                        l3, d3, speedRotation);
                else if (alpha3 > hitFractureMin)
                    RAM.Fracture((alpha3 - hitFractureMin) / speedRotation, ref arm3, ref arm4, ref cylinder3, ref piston3, ref fixed3,
                       ref alpha3, ref beta3, ref gamma3,
                       l3, d3, speedRotation);
            }
        }

        //Излом
        if (Input.GetKey(KeyCode.G))
        {
            if (allowRotation)
            {
                if (alpha3 <= hitFractureMax - speedRotation)
                    RAM.Fracture(-1, ref arm3, ref arm4, ref cylinder3, ref piston3, ref fixed3,
                       ref alpha3, ref beta3, ref gamma3,
                       l3, d3, speedRotation);
                else if (alpha3 < hitFractureMax)
                    RAM.Fracture((alpha3 - hitFractureMax) / speedRotation, ref arm3, ref arm4, ref cylinder3, ref piston3, ref fixed3,
                       ref alpha3, ref beta3, ref gamma3,
                       l3, d3, speedRotation);
            }

        }

        //Поворот четвертой стрелы с неподвижным изломом
        if (Input.GetKey(KeyCode.J))
        {
            if (allowRotation)
            {
                if (alpha5 >= hit5Min + speedRotation)
                    RAM.RotateArmHard_Pitch(1, ref arm5, ref cylinder5, ref piston5, ref clutch5, ref fixed5,
                        ref alpha5, ref beta5, ref gamma5, ref theta5, ref phi5, ref psi51, ref psi52,
                        OA5, OB5, OC5, BD5, CD5, speedRotation);
                else if (alpha5 > hit5Min)
                    RAM.RotateArmHard_Pitch((alpha5 - hit5Min) / speedRotation, ref arm5, ref cylinder5, ref piston5, ref clutch5, ref fixed5,
                        ref alpha5, ref beta5, ref gamma5, ref theta5, ref phi5, ref psi51, ref psi52,
                        OA5, OB5, OC5, BD5, CD5, speedRotation);
            }
        }

        //Поворот четвертой стрелы с неподвижным изломом
        if (Input.GetKey(KeyCode.H))
        {
            if (allowRotation)
            {
                if (alpha5 <= hit5Max - speedRotation)
                    RAM.RotateArmHard_Pitch(-1, ref arm5, ref cylinder5, ref piston5, ref clutch5, ref fixed5,
                       ref alpha5, ref beta5, ref gamma5, ref theta5, ref phi5, ref psi51, ref psi52,
                       OA5, OB5, OC5, BD5, CD5, speedRotation);
                else if (alpha5 < hit5Max)
                    RAM.RotateArmHard_Pitch((alpha5 - hit5Max) / speedRotation, ref arm5, ref cylinder5, ref piston5, ref clutch5, ref fixed5,
                       ref alpha5, ref beta5, ref gamma5, ref theta5, ref phi5, ref psi51, ref psi52,
                       OA5, OB5, OC5, BD5, CD5, speedRotation);
            }
        }

        //Удлинение пятой стрелы
        if (Input.GetKey(KeyCode.L))
        {
            if (allowRotation)
            {
                if (l6 <= hit6Max - speedElongation)
                    RAM.Elongation(1, ref arm6, ref l6, speedElongation);
                else if (l6 < hit6Max)
                    RAM.Elongation((hit6Max - l6) / speedElongation, ref arm6, ref l6, speedElongation);
            }
        }

        //Укорачивание пятой стрелы
        if (Input.GetKey(KeyCode.K))
        {
            if (allowRotation)
            {
                if (l6 >= hit6Min + speedElongation)
                    RAM.Elongation(-1, ref arm6, ref l6, speedElongation);
                else if (l6 > hit6Min)
                    RAM.Elongation((hit6Min - l6) / speedElongation, ref arm6, ref l6, speedElongation);
            }
        }

        //Тангаж НПУ
        if (Input.GetKey(KeyCode.X))
        {
            if (allowRotation)
            {
                if (alphaPitch >= hitPitchMin + speedRotation)
                    RAM.RotateArmHard_Pitch(1, ref pitcher, ref cylinder7, ref piston7, ref clutch7, ref fixed7,
                        ref alphaPitch, ref beta7, ref gamma7, ref theta7, ref phi7, ref psi71, ref psi72,
                        OA7, OB7, OC7, BD7, CD7, speedRotation);
                else if (alphaPitch > hitPitchMin)
                    RAM.RotateArmHard_Pitch((alphaPitch - hitPitchMin) / speedRotation, ref pitcher, ref cylinder7, ref piston7, ref clutch7, ref fixed7,
                        ref alphaPitch, ref beta7, ref gamma7, ref theta7, ref phi7, ref psi71, ref psi72,
                        OA7, OB7, OC7, BD7, CD7, speedRotation);
            }
        }

        //Тангаж НПУ
        if (Input.GetKey(KeyCode.Z))
        {
            if (allowRotation)
            {
                if (alphaPitch <= hitPitchMax - speedRotation)
                    RAM.RotateArmHard_Pitch(-1, ref pitcher, ref cylinder7, ref piston7, ref clutch7, ref fixed7,
                        ref alphaPitch, ref beta7, ref gamma7, ref theta7, ref phi7, ref psi71, ref psi72,
                        OA7, OB7, OC7, BD7, CD7, speedRotation);
                else if (alphaPitch < hitPitchMax)
                    RAM.RotateArmHard_Pitch((alphaPitch - hitPitchMax) / speedRotation, ref pitcher, ref cylinder7, ref piston7, ref clutch7, ref fixed7,
                        ref alphaPitch, ref beta7, ref gamma7, ref theta7, ref phi7, ref psi71, ref psi72,
                        OA7, OB7, OC7, BD7, CD7, speedRotation);
            }
        }

        //Крен НПУ
        if (Input.GetKey(KeyCode.V))
        {
            if (allowRotation)
            {
                if (alphaRoll >= hitRollMin + speedRotation)
                    RAM.RotateRoll(1, ref roller, ref cylinderRoller, ref pistonRoller,
                       ref alphaRoll, ref betaRoll, ref gammaRoll,
                       lRoll, dRoll, speedRotation);
                else if (alphaRoll > hitRollMin)
                    RAM.RotateRoll((alphaRoll - hitRollMin) / speedRotation, ref roller, ref cylinderRoller, ref pistonRoller,
                       ref alphaRoll, ref betaRoll, ref gammaRoll,
                       lRoll, dRoll, speedRotation);
            }
        }

        //Крен НПУ
        if (Input.GetKey(KeyCode.C))
        {
            if (allowRotation)
            {
                if (alphaRoll <= hitRollMax - speedRotation)
                    RAM.RotateRoll(-1, ref roller, ref cylinderRoller, ref pistonRoller,
                      ref alphaRoll, ref betaRoll, ref gammaRoll,
                      lRoll, dRoll, speedRotation);
                else if (alphaRoll < hitRollMax)
                    RAM.RotateRoll((alphaRoll - hitRollMax) / speedRotation, ref roller, ref cylinderRoller, ref pistonRoller,
                      ref alphaRoll, ref betaRoll, ref gammaRoll,
                      lRoll, dRoll, speedRotation);
            }
        }

        //Рыскание НПУ
        if (Input.GetKey(KeyCode.N))
        {
            if (allowRotation)
                //RAM.RotateYaw(1, ref yawer, ref alphaYaw, speedRotation);
                RAM.RotateBase_Yaw(1, ref yawer, ref alphaYaw, speedRotation);
        }

        //Рыскание НПУ
        if (Input.GetKey(KeyCode.B))
        {
            if (allowRotation)
                // RAM.RotateYaw(-1, ref yawer, ref alphaYaw, speedRotation);
                RAM.RotateBase_Yaw(-1, ref yawer, ref alphaYaw, speedRotation);
        }
        
        //Поднятие лап
        if (Input.GetKey(KeyCode.E))
        {
            if (Mathf.Abs(alphaFR) >= hitFootMin + speedRotation)
            {
                RAM.RaiseFoot(1,ref footFR, ref cylinderFR, ref pistonFR,
                    ref alphaFR, ref betaFR, ref gammaFR, lFR,dFR,speedRotation);
                RAM.RaiseFoot(1, ref footFL, ref cylinderFL, ref pistonFL,
                    ref alphaFL, ref betaFL, ref gammaFL, lFL, dFL, speedRotation);
                RAM.RaiseFoot(1, ref footBR, ref cylinderBR, ref pistonBR,
                    ref alphaBR, ref betaBR, ref gammaBR, lBR, dBR, speedRotation);
                RAM.RaiseFoot(1, ref footBL, ref cylinderBL, ref pistonBL,
                    ref alphaBL, ref betaBL, ref gammaBL, lBL, dBL, speedRotation);
            }
            else if (Mathf.Abs(alphaFR) > hitFootMin)
            {
                RAM.RaiseFoot((Mathf.Abs(alphaFR) - hitFootMin) / speedRotation, ref footFR, ref cylinderFR, ref pistonFR,
                   ref alphaFR, ref betaFR, ref gammaFR, lFR, dFR, speedRotation);
                RAM.RaiseFoot((Mathf.Abs(alphaFR) - hitFootMin) / speedRotation, ref footFL, ref cylinderFL, ref pistonFL,
                    ref alphaFL, ref betaFL, ref gammaFL, lFL, dFL, speedRotation);
                RAM.RaiseFoot((Mathf.Abs(alphaFR) - hitFootMin) / speedRotation, ref footBR, ref cylinderBR, ref pistonBR,
                    ref alphaBR, ref betaBR, ref gammaBR, lBR, dBR, speedRotation);
                RAM.RaiseFoot((Mathf.Abs(alphaFR) - hitFootMin) / speedRotation, ref footBL, ref cylinderBL, ref pistonBL,
                    ref alphaBL, ref betaBL, ref gammaBL, lBL, dBL, speedRotation);
            }
            allowRotation = false;
            allowMove = true;
        }

        //Опускание лап
        if (Input.GetKey(KeyCode.Q))
        {
            if (Mathf.Abs(alphaFR) <= hitFootMax - speedRotation)
            {
                RAM.RaiseFoot(-1, ref footFR, ref cylinderFR, ref pistonFR,
                     ref alphaFR, ref betaFR, ref gammaFR, lFR, dFR, speedRotation);
                RAM.RaiseFoot(-1, ref footFL, ref cylinderFL, ref pistonFL,
                    ref alphaFL, ref betaFL, ref gammaFL, lFL, dFL, speedRotation);
                RAM.RaiseFoot(-1, ref footBR, ref cylinderBR, ref pistonBR,
                    ref alphaBR, ref betaBR, ref gammaBR, lBR, dBR, speedRotation);
                RAM.RaiseFoot(-1, ref footBL, ref cylinderBL, ref pistonBL,
                    ref alphaBL, ref betaBL, ref gammaBL, lBL, dBL, speedRotation);
            }
            else if (Mathf.Abs(alphaFR) < hitFootMax)
            {
                RAM.RaiseFoot((Mathf.Abs(alphaFR) - hitFootMax) / speedRotation, ref footFR, ref cylinderFR, ref pistonFR,
                   ref alphaFR, ref betaFR, ref gammaFR, lFR, dFR, speedRotation);
                RAM.RaiseFoot((Mathf.Abs(alphaFR) - hitFootMax) / speedRotation, ref footFL, ref cylinderFL, ref pistonFL,
                    ref alphaFL, ref betaFL, ref gammaFL, lFL, dFL, speedRotation);
                RAM.RaiseFoot((Mathf.Abs(alphaFR) - hitFootMax) / speedRotation, ref footBR, ref cylinderBR, ref pistonBR,
                    ref alphaBR, ref betaBR, ref gammaBR, lBR, dBR, speedRotation);
                RAM.RaiseFoot((Mathf.Abs(alphaFR) - hitFootMax) / speedRotation, ref footBL, ref cylinderBL, ref pistonBL,
                    ref alphaBL, ref betaBL, ref gammaBL, lBL, dBL, speedRotation);
            }
            if (Mathf.Abs(alphaFR) < hitFootMax && Mathf.Abs(alphaFR) > hitFootMax - speedRotation)
            {
                allowRotation = true;
                allowMove = false;
            }
        }
    }

    // Привязка деталей
    void TieObjiects()
    {
        player = GameObject.Find("MainGameObject");
        body = GameObject.Find("body");
        cameras = GameObject.Find("cameras");

        // Связывание стрел
        {
            arm0 = GameObject.Find("arm0");
            arm1 = GameObject.Find("arm1");
            arm2 = GameObject.Find("arm2");
            arm3 = GameObject.Find("arm3");
            arm4 = GameObject.Find("arm4");
            arm5 = GameObject.Find("arm5");
            arm6 = GameObject.Find("arm6");

            link1 = GameObject.Find("link1");
            link2 = GameObject.Find("link2");
            link3 = GameObject.Find("link3");
            link4 = GameObject.Find("link4");
            link5 = GameObject.Find("link5");
            link6 = GameObject.Find("link6");

            cylinder1 = GameObject.Find("cylinder1");
            cylinder2 = GameObject.Find("cylinder2");
            cylinder3 = GameObject.Find("cylinder3");
            cylinder5 = GameObject.Find("cylinder5");
            cylinder7 = GameObject.Find("cylinder7");

            piston1 = GameObject.Find("piston1");
            piston2 = GameObject.Find("piston2");
            piston3 = GameObject.Find("piston3");
            piston5 = GameObject.Find("piston5");
            piston7 = GameObject.Find("piston7");


            fixed3 = GameObject.Find("fixed3");
            fixed5 = GameObject.Find("fixed5");
            fixed7 = GameObject.Find("fixed7");
            clutch5 = GameObject.Find("clutch5");
            clutch7 = GameObject.Find("clutch7");
            tong = GameObject.Find("tong");
        }

        //Связывание лап
        {
            footFR = GameObject.Find("footFR");
            cylinderFR = GameObject.Find("cylinderFR");
            pistonFR = GameObject.Find("pistonFR");

            footBL = GameObject.Find("footBL");
            cylinderBL = GameObject.Find("cylinderBL");
            pistonBL = GameObject.Find("pistonBL");

            footBR = GameObject.Find("footBR");
            cylinderBR = GameObject.Find("cylinderBR");
            pistonBR = GameObject.Find("pistonBR");

            footFL = GameObject.Find("footFL");
            cylinderFL = GameObject.Find("cylinderFL");
            pistonFL = GameObject.Find("pistonFL");
        }

        //Связывание НПУ
        {
            pitcher = GameObject.Find("pitcher");
            cylinderRoller = GameObject.Find("cylinderRoller");
            roller = GameObject.Find("roller");
            yawer = GameObject.Find("yawer");
            grip = GameObject.Find("grip");
            pistonRoller = GameObject.Find("pistonRoller");
        }
    }

    //Задание размеров
    void SetSize()
    {
        float h; //Переменная для вычисления изменяющихся сторон треугольников

        //Угол поворота базы
        {
            alpha0 = 0;
        }

        //Размеры для первой стрелы
        {
            h = OtherMath.DistanceYZ(cylinder1, piston1);
            l1 = OtherMath.DistanceYZ(cylinder1, link1);
            d1 = OtherMath.DistanceYZ(piston1, link1);
            alpha1 = Mathf.Acos((l1 * l1 + d1 * d1 - h * h) / (2 * l1 * d1));
            beta1 = OtherMath.CalcBeta(l1, d1, h, alpha1);
            gamma1 = PI - alpha1 - beta1;
        }

        //Размеры для второй стрелы
        {
            h = OtherMath.DistanceYZ(cylinder2, piston2);
            l2 = OtherMath.DistanceYZ(cylinder2, link2);
            d2 = OtherMath.DistanceYZ(piston2, link2);
            alpha2 = Mathf.Acos((l2 * l2 + d2 * d2 - h * h) / (2 * l2 * d2));
            beta2 = OtherMath.CalcBeta(l2, d2, h, alpha2);
            gamma2 = PI - alpha2 - beta2;
        }

        //Размеры для излома
        {
            cylinder3.transform.SetParent(arm3.transform);
            GameObject emptyArm = new GameObject();
            emptyArm.transform.localPosition = new Vector3(0, 0, 0);
            h = OtherMath.DistanceLocalYZ(cylinder3, piston3);
            l3 = OtherMath.DistanceLocalYZ(cylinder3, emptyArm);
            d3 = OtherMath.DistanceLocalYZ(piston3, emptyArm);
            alpha3 = Mathf.Acos((l3 * l3 + d3 * d3 - h * h) / (2 * l3 * d3));
            beta3 = OtherMath.CalcBeta(l3, d3, h, alpha3);
            gamma3 = PI - alpha3 - beta3;
            cylinder3.transform.SetParent(arm2.transform);
            Destroy(emptyArm);
        }

        //Размеры для четвертой стрелы
        {
            OA5 = OtherMath.DistanceYZ(fixed5, cylinder5);
            OB5 = OtherMath.DistanceYZ(fixed5, piston5);
            h = OtherMath.DistanceYZ(piston5, cylinder5);
            alpha5 = Mathf.Acos((OA5 * OA5 + OB5 * OB5 - h * h) / (2 * OB5 * OA5));
            beta5 = OtherMath.CalcBeta(OA5, OB5, h, alpha5);
            gamma5 = PI - alpha5 - beta5;
            OC5 = OtherMath.DistanceYZ(fixed5, link5);
            BD5 = OtherMath.DistanceYZ(piston5, clutch5);
            CD5 = OtherMath.DistanceYZ(link5, clutch5);
            h = OtherMath.DistanceYZ(piston5, link5);
            theta5 = Mathf.Acos((BD5 * BD5 + CD5 * CD5 - h * h) / (2 * BD5 * CD5));
            psi51 = Mathf.Acos((OC5 * OC5 + h * h - OB5 * OB5) / (2 * OC5 * h));
            psi52 = OtherMath.CalcBeta(CD5, BD5, h, theta5);
            phi5 = OtherMath.CalcBeta(OC5, h, OB5, psi51);
        }

        //Величина удлинения шестой стрелы
        {
            l6 = OtherMath.DistanceYZ(link5, link6);
        }

        //Размеры вычисления тангажа НПУ
        {
            OA7 = OtherMath.DistanceYZ(fixed7, cylinder7);
            OB7 = OtherMath.DistanceYZ(fixed7, piston7);
            h = OtherMath.DistanceYZ(piston7, cylinder7);
            alphaPitch = Mathf.Acos((OA7 * OA7 + OB7 * OB7 - h * h) / (2 * OB7 * OA7));
            beta7 = OtherMath.CalcBeta(OA7, OB7, h, alphaPitch);
            gamma7 = PI - alphaPitch - beta7;
            OC7 = OtherMath.DistanceYZ(fixed7, tong);
            BD7 = OtherMath.DistanceYZ(piston7, clutch7);
            CD7 = OtherMath.DistanceYZ(tong, clutch7);
            h = OtherMath.DistanceYZ(piston7, tong);
            theta7 = Mathf.Acos((BD7 * BD7 + CD7 * CD7 - h * h) / (2 * BD7 * CD7));
            psi71 = Mathf.Acos((OC7 * OC7 + h * h - OB7 * OB7) / (2 * OC7 * h));
            psi72 = OtherMath.CalcBeta(CD7, BD7, h, theta7);
            phi7 = OtherMath.CalcBeta(OC7, h, OB7, psi71);
        }

        //Размеры лап
        {
            h = OtherMath.DistanceXYZ(cylinderFR, pistonFR);
            lFR = OtherMath.DistanceXYZ(cylinderFR, footFR);
            dFR = OtherMath.DistanceXYZ(pistonFR, footFR);
            alphaFR = Mathf.Acos((lFR * lFR + dFR * dFR - h * h) / (2 * lFR * dFR));
            betaFR = OtherMath.CalcBeta(lFR, dFR, h, alphaFR);
            gammaFR = PI - alphaFR - betaFR;

            h = OtherMath.DistanceXYZ(cylinderFL, pistonFL);
            lFL = OtherMath.DistanceXYZ(cylinderFL, footFL);
            dFL = OtherMath.DistanceXYZ(pistonFL, footFL);
            alphaFL = Mathf.Acos((lFL * lFL + dFL * dFL - h * h) / (2 * lFL * dFL));
            betaFL = OtherMath.CalcBeta(lFL, dFL, h, alphaFL);
            gammaFL = PI - alphaFL - betaFL;

            h = OtherMath.DistanceXYZ(cylinderBR, pistonBR);
            lBR = OtherMath.DistanceXYZ(cylinderBR, footBR);
            dBR = OtherMath.DistanceXYZ(pistonBR, footBR);
            alphaBR = Mathf.Acos((lBR * lBR + dBR * dBR - h * h) / (2 * lBR * dBR));
            betaBR = OtherMath.CalcBeta(lBR, dBR, h, alphaBR);
            gammaBR = PI - alphaBR - betaBR;

            h = OtherMath.DistanceXYZ(cylinderBL, pistonBL);
            lBL = OtherMath.DistanceXYZ(cylinderBL, footBL);
            dBL = OtherMath.DistanceXYZ(pistonBL, footBL);
            alphaBL = Mathf.Acos((lBL * lBL + dBL * dBL - h * h) / (2 * lBL * dBL));
            betaBL = OtherMath.CalcBeta(lBL, dBL, h, alphaBL);
            gammaBL = PI - alphaBL - betaBL;
        }

        //Размеры для вычисления крена НПУ
        {
            h = OtherMath.DistanceXY(cylinderRoller, pistonRoller);
            lRoll = OtherMath.DistanceXY(roller, cylinderRoller);
            dRoll = OtherMath.DistanceXY(pistonRoller, roller);
            alphaRoll = Mathf.Acos((lRoll * lRoll + dRoll * dRoll - h * h) / (2 * lRoll * dRoll));
            betaRoll = OtherMath.CalcBeta(lRoll, dRoll, h, alphaRoll);
            gammaRoll = PI - alphaRoll - betaRoll;
        }

        //Угол рыскания НПУ
        {
            alphaYaw = 0;
        }
    }

    //Задание ограничений на повороты и перемещения
    void SetHits()
    {
        hitFootMin = (alphaBL + alphaBR + alphaFR + alphaFL) / 4;
        hitFootMax = 93 * Mathf.Deg2Rad;
        hit1Max = 135 * Mathf.Deg2Rad;
        hit1Min = 31 * Mathf.Deg2Rad;
        hit2Max = 121 * Mathf.Deg2Rad;
        hit2Min = 41 * Mathf.Deg2Rad;
        hitFractureMin = 29 * Mathf.Deg2Rad;
        hitFractureMax = 130 * Mathf.Deg2Rad;
        hit5Max = 118 * Mathf.Deg2Rad;
        hit5Min = 19 * Mathf.Deg2Rad;
        hit6Min = l6;
        hit6Max = 1.88f;
        hitPitchMin = 26 * Mathf.Deg2Rad;
        hitPitchMax = 153 * Mathf.Deg2Rad;
        hitRollMin = 25 * Mathf.Deg2Rad;
        hitRollMax = 118 * Mathf.Deg2Rad;
    }
}