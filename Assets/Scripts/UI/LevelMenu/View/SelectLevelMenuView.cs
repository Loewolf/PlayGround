using System;
using Controllers;
using Core;
using UnityEngine;

namespace View
{
    public class SelectLevelMenuMenuView : BaseView<ISelectLevelMenuView>, ISelectLevelMenuView, IMenuView
    {
        //private PauseMenuView _pauseMenuView;
        protected override ISelectLevelMenuView View => this;
        public event Action BackEvent;
        public event Action<IController<IPauseMenuView>> OpenPauseEvent;

        //public void SetView(_pause)
        public void Back()
        {
            BackEvent?.Invoke();
            OpenPauseEvent?.Invoke(new PauseMenuController());
            OpenPauseEvent -= (Action<IController<IPauseMenuView>>) OpenPauseEvent?.GetInvocationList()[0];
        }
    }
}
