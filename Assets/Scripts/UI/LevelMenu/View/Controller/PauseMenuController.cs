using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Controllers
{
    public interface IPauseMenuView : IView
    {
        event Action BackEvent;
        event Action ResumeEvent;
        event Action RestartEvent;
        event Action GoMainMenuEvent;
        event Action<IView> SelectLevelEvent;
    }
    public class PauseMenuController : IController<IPauseMenuView>
    {
        private IPauseMenuView _view;

        public void OnOpen(IPauseMenuView view)
        {
            _view = view;
            _view.ResumeEvent += Resume;
            _view.SelectLevelEvent += SelectLevels;
        }

        public void OnClose(IPauseMenuView view)
        {
            _view.ResumeEvent -= Resume;
            _view.SelectLevelEvent -= SelectLevels;
            _view = null;
        }
        
        public void Resume()
        {
            _view.Close(this);
        }
        public void StartAgain()
        {
        
        }

        public void MainMenu()
        {
        
        }

        public void SelectLevels(IView selectLvlView)
        {
            selectLvlView.Open(new SelectLevelMenuController());
            _view.Close(this);
        }
    }
}
