using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class MainBoxes
{
    public Vector3 pos;
    public Vector3 size;
    public Quaternion q;

    public MainBoxes(GameObject kub)
    {
        pos = kub.transform.position;
        size = kub.transform.lossyScale;
        q = kub.transform.rotation;
    }

    public void upd(GameObject baseObj)
    {
        
        pos = baseObj.transform.position;
        q =  baseObj.transform.rotation;
        pos = baseObj.transform.position+(pos - baseObj.transform.position);
        
        /*
        pos += -baseObj.transform.position;
        pos = BoxCollision.QuanRotation(pos, q);
        pos += baseObj.transform.position;*/
    }
    
}
public class BoxMainObject : MonoBehaviour
{
    public GameObject[] robot = new GameObject[10];
    //комментариями помечена диагональ, что бы удобней читалось
    private static byte[,] matrixCross = 
        //                     0   1  2  3  4  5  6  7  8  9 
        {   /*0 корпус*/       {0, 0, 0, 0, 0, 0, 0, 1, 1, 1}, 
            /*1 корпус*/       {0, 0, 0, 0, 0, 0, 0, 1, 1, 1},
            /*2 лапа1*/        {0, 0, 0, 0, 0, 0, 1, 1, 1, 1},
            /*3 лапа2*/        {0, 0, 0, 0, 0, 0, 1, 1, 1, 1},
            /*4 лапа3*/        {0, 0, 0, 0, 0, 0, 1, 1, 1, 1},
            /*5 лапа4*/        {0, 0, 0, 0, 0, 0, 0, 1, 1, 1},
            /*6 осн. стрелы*/  {0, 0, 1, 1, 1, 0, 0, 0, 1, 1},
            /*7 стрела1*/      {1, 1, 1, 1, 1, 1, 0, 0, 0, 1},
            /*8 стрела2*/      {1, 1, 1, 1, 1, 1, 1, 0, 0, 0},
            /*9 стрела3*/      {1, 1, 1, 1, 1, 1, 1, 1, 0, 0},
        };


    void Update()
    {
       DetectAllCollission();
    }

    public bool DetectAllCollission()
    {
        for(int i = 0;i<10;i++)
        {
            for(int j =0;j<10;j++)
            {
                if(matrixCross[i,j] == 1)
                {
                    //проверяем
                    Vector3 result = BoxCollision.Collision(robot[i], robot[j]);
                    if (result != Vector3.zero)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
}
