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
        event Action RestartEvent;
        event Action GoMainMenuEvent;
        event Action SelectLevelEvent;
    }
    public class PauseMenuController : IController<IPauseMenuView>
    {
        private IPauseMenuView _view;

        public void OnOpen(IPauseMenuView view)
        {
            _view = view;
            _view.BackEvent += Resume;
        }

        public void OnClose(IPauseMenuView view)
        {
            _view.BackEvent -= Resume;
            _view = null;
        }
        
        public void Resume()
        {
            _view.Close(this);
        }
    }
}
