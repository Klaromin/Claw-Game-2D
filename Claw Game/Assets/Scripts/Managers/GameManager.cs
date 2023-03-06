using System;
using System.Threading.Tasks;
using UnityEngine;


namespace Minigame.Presentation
{
    public class GameManager : MonoBehaviour
    {
        #region Public Properities
    
        public static GameManager Instance;
        public GameState State { get; private set; }
        public static event Action<GameState> GameStateChangedEvent;
    
        #endregion
    
        #region Private fields
    
        [SerializeField] private GameView _gameView;
        private GameController _controller;
    
        #endregion
    
        #region Unity: Awake | Start | OnDisable
    
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            UpdateGameState(GameState.Initial);
            InitializeInnerMVC();
        }

        private void OnDisable()
        {
            DeInitializeInnerMVC();
        }
    
        #endregion
    
        #region MVC: InitializeInnerMVC | DeInitializeInnerMVC
    
        private void InitializeInnerMVC()
        {
            GameModel gameModel = new GameModel();
            GameView gameView = _gameView;
            GameController controller = new GameController(gameModel, gameView);
            _controller = controller;
            controller.Init();
        }
    
        private void DeInitializeInnerMVC()
        {
            _controller.DeInit();
        }
    
        #endregion
    
        #region Event: UpdateGameState
        public void UpdateGameState(GameState newState)
        {
            State = newState;
            switch (newState)
            {
                case GameState.Initial:
                    break;
                case GameState.Movement:
                    HandleMovement();
                    break;
                case GameState.Decision:
                    HandleDecision();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
            GameStateChangedEvent?.Invoke(newState); 
        }
    
        #endregion
    
        #region Methods: HandleMovement | HandleDecision
    
        private async void HandleMovement()
        {
            await Task.Delay(5000);
            UpdateGameState(GameState.Decision);
        }

        private async void HandleDecision()
        {
            await Task.Delay(3000);
            UpdateGameState(GameState.Initial);
        }
    
        #endregion
    }
}