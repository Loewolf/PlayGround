using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using View;

namespace Controllers
{
    public interface IPauseMenuView : IMenuView
    {
        event Action ResumeEvent;
        event Action<IMenuView> RestartEvent;
        event Action GoMainMenuEvent;
        event Action<IMenuView> SelectLevelEvent;
    }
    public class PauseMenuController : IController<IPauseMenuView>
    {
        private IPauseMenuView _view;

        public void OnOpen(IPauseMenuView view)
        {
            _view = view;
            _view.ResumeEvent += Resume;
            _view.RestartEvent += Restart;
            _view.SelectLevelEvent += SelectLevels;
        }

        public void OnClose(IPauseMenuView view)
        {
            _view.ResumeEvent -= Resume;
            _view.RestartEvent -= Restart;
            _view.SelectLevelEvent -= SelectLevels;
            _view = null;
        }
        
        public void Resume()
        {
            _view.MenuManager.Resume();
            _view.MenuManager.RemoveStackView();
            _view.Close(this);
        }
        public void Restart(IMenuView restartView)
        {
            _view.MenuManager.AddStackView(restartView);
            restartView.Open();
            _view.Close(this);
        }

        public void MainMenu()
        {
        
        }

        public void SelectLevels(IMenuView selectLvlView)
        {
            _view.MenuManager.AddStackView(selectLvlView);
            selectLvlView.Open();
            _view.Close(this);
        }
    }
}
