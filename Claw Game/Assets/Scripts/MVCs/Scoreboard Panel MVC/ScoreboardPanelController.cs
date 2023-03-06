using System;
using Minigame.Data;

namespace Minigame.Presentation
{
    public class ScoreboardPanelController : BaseController<ScoreboardPanelModel, ScoreboardPanelView>
    {
        #region EventHandler
        
        public event EventHandler<ClawMoveCompleteArgs> OnClawVerticalMovementCompleteEvent; 
        
        #endregion
        
        #region Constructor
        public ScoreboardPanelController(ScoreboardPanelModel model, ScoreboardPanelView view) : base(model, view)
        {
        }
        
        #endregion
        
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
    
        #region Event: OnVerticalMovementComplete
        
        public void OnVerticalClawMovementComplete(object sender, ClawMoveCompleteArgs e)
        { 
            OnClawVerticalMovementCompleteEvent?.Invoke(sender, e);
        }
        
        #endregion
    
        #region Events: Add | Remove
        
        private void AddEvents()
        {
            OnClawVerticalMovementCompleteEvent += View.OnClawVerticalClawVerticalMoveComplete;
        }
    
        private void RemoveEvents()
        {
            OnClawVerticalMovementCompleteEvent -= View.OnClawVerticalClawVerticalMoveComplete;
        }
    
        #endregion
    }
  
}