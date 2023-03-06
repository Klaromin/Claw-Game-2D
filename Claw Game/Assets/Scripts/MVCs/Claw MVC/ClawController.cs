using System;
using Minigame.Data;

namespace Minigame.Presentation
{
    public class ClawController : BaseController<ClawModel, ClawView>
    {
        #region Events
        
        public event EventHandler<ClawMoveCompleteArgs> OnVerticalClawMovementCompleteEvent; 
        
        #endregion
        
        #region Constructor
        
        public ClawController(ClawModel model, ClawView view) : base(model, view) 
        {
        }
        
        #endregion
    
        #region MVC: OnInit | OnDeInit
        
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
        
        public void OnStickMoved(object sender, StickMovementArgs e)
        {
            Model.ClawSpeed = e.speedMultiplier;
        }
        
        #endregion
    
        #region Event: OnButtonClick
        
        public void OnGameButtonClick(object sender, EventArgs e) //isimi düzelt.
        {
            View.Dive();
        }
        
        #endregion
    
        #region Event: OnMovementComplete
        
        private void OnClawHorizontalMoveComplete(object sender, ClawMoveCompleteArgs e)
        {
            OnVerticalClawMovementCompleteEvent?.Invoke(sender, e);
        }
        
        #endregion
   
        #region Events: Add | Remove
        
        private void AddEvents()
        {
            View.OnVerticalClawMovementCompleteEvent += OnClawHorizontalMoveComplete;
        }
        private void RemoveEvents()
        {
            View.OnVerticalClawMovementCompleteEvent += OnClawHorizontalMoveComplete;
        }
        
        #endregion
    }
}