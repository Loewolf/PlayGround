using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Core;
using UnityEngine;
using View;

public class MenuManager : MonoBehaviour
{
    public PauseMenuView PauseMenuView;
    private Stack<IMenuView> _thisView = new Stack<IMenuView>();
    
    public KeyCode PauseButton;
    public KeyCode BackButton;

    public void Pause()
    {
        _thisView.Push(PauseMenuView);
        PauseMenuView.SetGameManager(this);
        PauseMenuView.Open(new PauseMenuController());
    }

    public void Resume()
    {
        PauseMenuView.SetGameManager(null);
    }

    public void Back()
    {
        _thisView.Pop().Back();
    }

    public void AddView(IMenuView view)
    {
        _thisView.Push(view);
    }
    public void Update()
    {
        if (Input.GetKeyDown(PauseButton))
        {
            Pause();
        }
    }
}
