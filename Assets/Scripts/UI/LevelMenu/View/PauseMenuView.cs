using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Core;
using UnityEngine;

namespace View
{

    public class PauseMenuView : BaseView<IPauseMenuView>, IPauseMenuView, IMenuView
    {
        private MenuManager MenuManager;

        public RestartMenuView RestartMenuView;
        public SelectLevelMenuMenuView SelectLevelMenuMenuView;

        protected override IPauseMenuView View => this;
        public event Action BackEvent;
        public event Action ResumeEvent;
        public event Action GoMainMenuEvent;
        public event Action RestartEvent;
        public event Action<IView> SelectLevelEvent;


        public void SetGameManager(MenuManager gm)
        {
            MenuManager = gm;
        }

        public void Resume()
        {
            ResumeEvent?.Invoke();
            MenuManager?.Resume();
        }

        public void StartAgain()
        {

        }

        public void MainMenu()
        {

        }

        public void SelectLevels()
        {
            MenuManager.AddView(SelectLevelMenuMenuView);
            SelectLevelEvent?.Invoke(SelectLevelMenuMenuView);
            SelectLevelMenuMenuView.OpenPauseEvent += Open;
        }

        public void Back()
        {
            Resume();
        }

    }
}
