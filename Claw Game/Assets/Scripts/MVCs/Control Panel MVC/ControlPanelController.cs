using System;
using Minigame.Data;

namespace Minigame.Presentation
{
    public class ControlPanelController : BaseController<ControlPanelModel, ControlPanelView>
    {
        #region EventHandlers
        public event EventHandler<StickMovementArgs> OnStickMovedEvent;
        public event EventHandler OnGameButtonClickedEvent; 
        #endregion
        public ControlPanelController(ControlPanelModel model, ControlPanelView view) : base(model, view)
        {
        }
    
        #region MVC: OnInit
        protected override void OnInit()
        {
            AddEvents();
        }

        protected override void OnDeInit()
        {
            RemoveEvents();
        }

        #endregion
    
        #region Event: OnStickMoved
        private void OnStickMoved(object sender, StickMovementArgs e)
        {
            OnStickMovedEvent?.Invoke(sender,e);
        }
        #endregion
    
        #region Event: OnButtonClicked
        private void OnButtonClicked(object sender, EventArgs e)
        {
            OnGameButtonClickedEvent?.Invoke(sender, e);
        }
        #endregion
    
        #region Events: Add | Remove
        private void AddEvents()
        {
            View.OnStickMovedEvent += OnStickMoved;
            View.OnButtonClickedEvent += OnButtonClicked;
        }

        private void RemoveEvents()
        {
            View.OnStickMovedEvent -= OnStickMoved;
            View.OnButtonClickedEvent -= OnButtonClicked;
        }
        #endregion
    }  
}