using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour {
    
    public Camera[] cameraList;
    private int currentCamera;

    void Start()
    {
        currentCamera = 0;
        for (int i = 0; i < cameraList.Length; i++)
        {
            cameraList[i].gameObject.SetActive(false);
        }

        if (cameraList.Length > 0)
        {
            cameraList[0].gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            currentCamera++;
            if (currentCamera < cameraList.Length)
            {
                cameraList[currentCamera - 1].gameObject.SetActive(false);
                cameraList[currentCamera].gameObject.SetActive(true);
            }
            else
            {
                cameraList[currentCamera - 1].gameObject.SetActive(false);
                currentCamera = 0;
                cameraList[currentCamera].gameObject.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            currentCamera--;
            if (currentCamera >= 0)
            {
                cameraList[currentCamera + 1].gameObject.SetActive(false);
                cameraList[currentCamera].gameObject.SetActive(true);
            }
            else
            {
                cameraList[currentCamera + 1].gameObject.SetActive(false);
                currentCamera = cameraList.Length - 1;
                cameraList[currentCamera].gameObject.SetActive(true);
            }
        }
    }
}
