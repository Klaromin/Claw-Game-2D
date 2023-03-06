using System;
using Minigame.Data;

namespace Minigame.Presentation
{
    public class GameplayController : BaseController<GameplayModel,GameplayView>
    {
        #region EventHandlers
        
        public event EventHandler<StickMovementArgs> OnStickMovedEvent;
        public event EventHandler OnGameButtonClickedEvent;
        public event EventHandler<ClawMoveCompleteArgs> OnVerticalClawMovementCompleteEvent;
        
        #endregion
    
        #region Controllers
        
        private ClawController _clawController;
        
        #endregion
        
        #region Constructor
        public GameplayController(GameplayModel model, GameplayView view) : base(model, view)
        {
        }
        
        #endregion
        
        #region MVC: OnInit | InitializeClawMVC
        
        protected override void OnInit()
        {
            InitializeClawMVC();
            AddEvents();
        }

        protected override void OnDeInit()
        {
            DeInitializeClawMVC();
            RemoveEvents();
        }

        private void DeInitializeClawMVC()
        {
            _clawController.DeInit();
        }

        private void InitializeClawMVC()
        {
            var model = new ClawModel();
            var view = View.ClawView;
            var controller = new ClawController(model,view);
            _clawController = controller;
            controller.Init();
        }
        
        #endregion 
    
        #region Event: OnStickMoved
        
        public void OnStickMoved(object sender, StickMovementArgs e)
        {
            OnStickMovedEvent?.Invoke(sender, e);
        }
        
        #endregion
    
        #region OnGameButtonClick
        
        public void OnGameButtonClick(object sender, EventArgs e)
        {
            OnGameButtonClickedEvent?.Invoke(sender, e);
        }
        
        #endregion
    
        #region Event: OnVerticalClawMovementComplete
        
        public void OnVerticalClawMovementComplete(object sender, ClawMoveCompleteArgs e)
        {
            OnVerticalClawMovementCompleteEvent?.Invoke(sender, e);
        }
        
        #endregion
    
        #region Events: Add | Remove
        
        private void AddEvents()
        {
            _clawController.OnVerticalClawMovementCompleteEvent += OnVerticalClawMovementComplete;
            OnVerticalClawMovementCompleteEvent += View.OnVerticalClawMovementComplete;
            OnGameButtonClickedEvent += _clawController.OnGameButtonClick;
            OnStickMovedEvent += _clawController.OnStickMoved;
        }
    
        private void RemoveEvents()
        {
            _clawController.OnVerticalClawMovementCompleteEvent += OnVerticalClawMovementComplete;
            OnVerticalClawMovementCompleteEvent += View.OnVerticalClawMovementComplete;
            OnGameButtonClickedEvent += _clawController.OnGameButtonClick;
            OnStickMovedEvent += _clawController.OnStickMoved;
        }
        
        #endregion
    }
}