using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;

public class PauseMenuView : BaseView<IPauseMenuView>,IPauseMenuView
{
    public RestartMenuView RestartMenuView;
    public SelectLevelMenuView SelectLevelMenuView;
    
    protected override IPauseMenuView View => this;
    public event Action BackEvent;
    public event Action GoMainMenuEvent;
    public event Action RestartEvent;
    public event Action SelectLevelEvent;

    public void Resume()
    {
        BackEvent?.Invoke();
    }

    public void StartAgain()
    {
        
    }

    public void MainMenu()
    {
        
    }

    public void SelectLevels()
    {
    }
    
}
