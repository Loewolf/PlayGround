using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Controllers
{
    public interface ISelectLevelMenuView : IView
    {
        event Action BackEvent;
        event Action<IController<IPauseMenuView>> OpenPauseEvent;
    }
    public class SelectLevelMenuController : IController<ISelectLevelMenuView>
    {
        private ISelectLevelMenuView menuView;
        
        public void OnOpen(ISelectLevelMenuView menuView)
        {
            this.menuView = menuView;
            this.menuView.BackEvent += Back;
        }

        public void OnClose(ISelectLevelMenuView menuView)
        {
            this.menuView.BackEvent -= Back;
            this.menuView = null;
        }

        public void Back()
        {
            menuView.Close(this);
        }
        
    }
}
