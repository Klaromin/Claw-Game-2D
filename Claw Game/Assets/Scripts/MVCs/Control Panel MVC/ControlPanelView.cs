using System;
using UnityEngine;
using UnityEngine.UI;
using Minigame.Data;
namespace Minigame.Presentation
{
    public class ControlPanelView : BaseView<ControlPanelModel>
    {
        #region EventHandlers
        public event EventHandler<StickMovementArgs> OnStickMovedEvent;
        public event EventHandler OnButtonClickedEvent; 
        #endregion

        #region SerializeFields
        [Header("Transforms")] 
        [SerializeField] private RectTransform _rotator;
        [Header("Cameras")]
        [SerializeField] private Camera _camera;

        [SerializeField] private Canvas _gameCanvas;

        [SerializeField] private Button _gameButton;
        [SerializeField] private Animator _animator;
        [SerializeField] private Image _stickBoundary;
        #endregion
    
        #region Private Variables
        private Vector3 _mousePosition;
        private Vector3 _direction;
    
        #endregion
    
        #region Unity Functions: Update | OnDisable

        private void Update()
        {
            StickMovement();
        }

        private void OnDisable()
        {
            RemoveEvents();
        }

        #endregion
    
        #region MVC: Init | InitButton
        public override void Init()
        {
        
            AddEvents();
        }

        public override void DeInit()
        {
            RemoveEvents();
        }


        #endregion
    
        #region Method: StickMovement
        private void StickMovement()
        {
            _mousePosition = Input.mousePosition;
            var mouseUIPosition = CanvasPositioningExtensions.ScreenToCanvasPosition(_gameCanvas, _mousePosition);
            Vector2 direction = new Vector2( mouseUIPosition.x -_rotator.transform.position.x  , mouseUIPosition.y -  _rotator.transform.position.y);
            var transformRotation = _rotator.transform.rotation;
            float degreeRotation = (float)((180 / Math.PI) * transformRotation.z);
            if (Input.GetMouseButton(0))
            {
                if (direction.x <_stickBoundary.rectTransform.rect.width && direction.x > -_stickBoundary.rectTransform.rect.width && direction.y <_stickBoundary.rectTransform.rect.height && direction.y >_stickBoundary.rectTransform.rect.height/2)
                {
                    _rotator.transform.up = direction.normalized ; 
                } 

            }
            else
            {
                _rotator.transform.up = Vector3.up; 
            }
            OnStickMovedEvent?.Invoke(this, new StickMovementArgs()
            {
                speedMultiplier = degreeRotation
            });
        }
        #endregion
    
        #region Event: OnGameStataChanged
        private void OnGameStateChanged(GameState state)
        {
            _gameButton.interactable = state == GameState.Initial;
        }
        #endregion
    
        #region Event: OnGameButtonClick
        
        private void OnGameButtonClick()
        {
            _animator.SetTrigger("Click");
            GameManager.Instance.UpdateGameState(GameState.Movement);
            OnButtonClickedEvent?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    
        #region Events: Add | Remove
        private void AddEvents()
        {
            _gameButton.onClick.AddListener(OnGameButtonClick);
            GameManager.GameStateChangedEvent += OnGameStateChanged;
        }
 
        private void RemoveEvents()
        {
            GameManager.GameStateChangedEvent -= OnGameStateChanged;
        }
        #endregion
    }
  
}