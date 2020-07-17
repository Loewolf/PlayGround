using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PauseMenuView PauseMenuView;

    public KeyCode PauseButton;
    public KeyCode BackButton;

    public void Pause()
    {
        //var controller = new PauseMenuController();
        PauseMenuView.Open(new PauseMenuController());
        PauseMenuView.BackEvent += Resume;
    }

    public void Resume()
    {
        
    }

    public void Update()
    {
        if (Input.GetKeyDown(PauseButton))
        {
            Pause();
        }
    }
}
