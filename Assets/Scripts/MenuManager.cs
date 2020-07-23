using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Core;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PauseMenuView PauseMenuView;
    private BaseView<IView> _thisView;
    
    public KeyCode PauseButton;
    public KeyCode BackButton;

    public void Pause()
    {
        PauseMenuView.SetGameManager(this);
        PauseMenuView.Open(new PauseMenuController());
    }

    public void Resume()
    {
        PauseMenuView.SetGameManager(null);
    }

    public void Back()
    {
        //_thisView.Back();
    }
    public void Update()
    {
        if (Input.GetKeyDown(PauseButton))
        {
            Pause();
        }
    }
}
