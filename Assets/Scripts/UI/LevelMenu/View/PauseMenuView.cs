using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace View
{

    public class PauseMenuView : BaseView<IPauseMenuView>, IPauseMenuView
    {
        public MenuManager MenuManager{ get; set; }

        protected override IPauseMenuView View => this;
        
        public event Action ResumeEvent;
        public event Action<IMenuView> RestartEvent;
        public event Action GoMainMenuEvent;
        public event Action<IMenuView> SelectLevelEvent;



        protected override IController<T> CreateController<T>()
        {
            return (IController<T>) new PauseMenuController();
        }

        public void Resume()
        {
            
            ResumeEvent?.Invoke();
        }

        public void Restart()
        {
            RestartEvent?.Invoke(MenuManager.RestartMenuView);
        }

        public void MainMenu()
        {

        }

        public void SelectLevels()
        {
            SelectLevelEvent?.Invoke(MenuManager.SelectLevelMenuMenuView);
        }

        public  void Back()
        {
            Resume();
        }

    }
}
