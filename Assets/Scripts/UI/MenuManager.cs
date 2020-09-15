using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Core;
using UnityEngine;
using View;

public class MenuManager : MonoBehaviour
{
    [Header("Views")]
    public PauseMenuView PauseMenuView;
    public RestartMenuView RestartMenuView;
    public SelectLevelMenuView SelectLevelMenuMenuView;
    
    private Stack<IMenuView> _thisStackView = new Stack<IMenuView>();
    
    [Header("Buttons")]
    public KeyCode PauseButton;
    public KeyCode BackButton;

    
    private void Awake()
    {
        PauseMenuView.MenuManager = this;
        SelectLevelMenuMenuView.MenuManager = this;
        RestartMenuView.MenuManager = this;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        AddStackView(PauseMenuView);
        PauseMenuView.Open();
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    public void Back()
    {
        _thisStackView.Peek().Back();
    }

    public IView GetView()
    {
        return _thisStackView.Peek();
    }

    public void AddStackView(IMenuView view)
    {
        _thisStackView.Push(view);
    }

    public void RemoveStackView()
    {
        _thisStackView?.Pop();
    }
    public void Update()
    {
        if (Input.GetKeyDown(PauseButton) && _thisStackView.Count == 0) 
        {
            Pause();
        }

        if (Input.GetKeyDown(BackButton) &&  _thisStackView.Count != 0)
        {
            Back();
        }
    }
}
