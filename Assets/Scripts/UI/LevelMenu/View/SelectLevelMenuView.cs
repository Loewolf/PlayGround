using System;
using Controllers;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class SelectLevelMenuView : BaseView<ISelectLevelMenuView>, ISelectLevelMenuView
    {
        public MenuManager MenuManager{ get; set; }
        protected override ISelectLevelMenuView View => this;
        public event Action BackEvent;

        protected override IController<T> CreateController<T>()
        {
            return (IController<T>) new SelectLevelMenuController();
        }
        
        public void Back()
        {
            BackEvent?.Invoke();
            
        }
        
    }
}
