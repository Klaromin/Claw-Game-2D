using System;
using Minigame.Data;

namespace Minigame.Presentation
{
    public class GameController : BaseController<GameModel, GameView>
    {
        #region EventHandlers
        
        public event EventHandler<StickMovementArgs> OnStickMovedEvent;
        public event EventHandler OnGameButtonClickedEvent;
        public event EventHandler<ClawMoveCompleteArgs> OnVerticalClawMovementCompleteEvent; 
        
        #endregion
    
        #region Controllers
        
        private ControlPanelController _cpController;
        private ScoreboardPanelController _spController;
        private GameplayController _gpController;
        
        #endregion

        #region Constructor

        public GameController(GameModel model, GameView view) : base(model, view)
        {
        }

        #endregion

        #region MVC: OnInit | OnDeInit |InitializeInnerMVCs | DeInitializeInnverMVCs
        
        protected override void OnInit()
        {
            InitializeInnerMVCs();
            AddEvents();
        }

        protected override void OnDeInit()
        {
            RemoveEvents();
            DeInitializeInnverMVCs();
        }

        private void DeInitializeInnverMVCs()
        {
            DeInitializeControllerPanelMVC();
            DeInitializeScoreBoardMVC();
            DeInintializeGameplayMVC();
        }
        
        private void InitializeInnerMVCs()
        {
            InitializeControllerPanelMVC();
            InitializeScoreBoardMVC();
            InintializeGameplayMVC();
        }
        
        #endregion
    
        #region MVC: InitializeGameplayMVC | InitializeScoreBoardMVC | InitializeControllerPanelMVC
        
        private void InintializeGameplayMVC()
        {
            GameplayModel model = new GameplayModel();
            GameplayView view = View.GPView;
            GameplayController controller = new GameplayController(model, view);
            _gpController = controller;
            controller.Init();
        }

        private void InitializeScoreBoardMVC()
        {
            ScoreboardPanelModel model = new ScoreboardPanelModel();
            ScoreboardPanelView view = View.SPView;
            ScoreboardPanelController controller = new ScoreboardPanelController(model,view);
            _spController = controller;
            controller.Init();
        }

        private void InitializeControllerPanelMVC()
        {
            ControlPanelModel model = new ControlPanelModel();
            ControlPanelView view = View.CPView;
            ControlPanelController controller = new ControlPanelController(model,view);
            _cpController = controller;
            controller.Init();
        }
        
        #endregion
    
        #region MVC: DeInitializeGameplayMVC | DeInitializeScoreBoardMVC | DeInitializeControllerPanelMVC
        
        private void DeInintializeGameplayMVC()
        {
            _gpController.DeInit();
        }

        private void DeInitializeScoreBoardMVC()
        {
            _cpController.DeInit();
        }

        private void DeInitializeControllerPanelMVC()
        {
            _spController.DeInit();
        }
        
        #endregion
        
        #region Event: OnStickMoved
        
        private void OnStickMoved(object sender, StickMovementArgs e)
        {
            Model.CamSpeed = e.speedMultiplier;
            OnStickMovedEvent?.Invoke(sender,e);
        }
        
        #endregion
        
        #region Event: OnGameButtonClicked
        
        private void OnGameButtonClick(object sender , EventArgs e)
        {
            OnGameButtonClickedEvent?.Invoke(sender, e);
        }
        
        #endregion
        
        #region Event: OnClawHorizontalMovementComplete
        
        private void OnVerticalClawMovementComplete(object sender, ClawMoveCompleteArgs e)
        {
            OnVerticalClawMovementCompleteEvent?.Invoke(sender, e);
        }
        
        #endregion
    
        #region Events: Add | Remove
        
        private void AddEvents()
        {
            OnVerticalClawMovementCompleteEvent += _spController.OnVerticalClawMovementComplete;
            _gpController.OnVerticalClawMovementCompleteEvent += OnVerticalClawMovementComplete;
            OnStickMovedEvent += _gpController.OnStickMoved;
            _cpController.OnStickMovedEvent += OnStickMoved;
            OnGameButtonClickedEvent += _gpController.OnGameButtonClick;
            _cpController.OnGameButtonClickedEvent += OnGameButtonClick;
            
        }
        private void RemoveEvents()
        {
            OnVerticalClawMovementCompleteEvent -= _gpController.OnVerticalClawMovementComplete; 
            _spController.OnClawVerticalMovementCompleteEvent -= OnVerticalClawMovementComplete;
            OnStickMovedEvent -= _gpController.OnStickMoved;
            _cpController.OnStickMovedEvent -= OnStickMoved;
            OnGameButtonClickedEvent -= _gpController.OnGameButtonClick;
            _cpController.OnGameButtonClickedEvent -= OnGameButtonClick;
        }
        
        #endregion

    }
}