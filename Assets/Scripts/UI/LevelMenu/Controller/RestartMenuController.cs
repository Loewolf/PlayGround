using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using View;

public interface IRestartMenuView : IMenuView
{
    event Action RestartEvent;
    event Action BackEvent;
}
public class RestartMenuController : IController<IRestartMenuView>
{
    private IRestartMenuView _view;
    public void OnOpen(IRestartMenuView view)
    {
        _view = view;
        _view.RestartEvent += Restart;
        _view.BackEvent += Back;
    }

    public void OnClose(IRestartMenuView view)
    {
        _view.RestartEvent -= Restart;
        _view.BackEvent -= Back;
        _view = null;
    }

    public void Restart()
    {
        
    }

    public void Back()
    {
        _view.MenuManager.RemoveStackView();
        _view.MenuManager.GetView().Open();
        _view.Close(this);
    }
}
