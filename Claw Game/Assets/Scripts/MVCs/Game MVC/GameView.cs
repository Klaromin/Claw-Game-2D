using UnityEngine;


namespace Minigame.Presentation
{
    public class GameView : BaseView<GameModel>
    {
        #region Views
   
        [SerializeField] private ControlPanelView _cpView;
        public ControlPanelView CPView => _cpView;
    
        [SerializeField] private ScoreboardPanelView _spView;
        public ScoreboardPanelView SPView => _spView;
    
        [SerializeField] private GameplayView _gpView;
        public GameplayView GPView => _gpView;
        
        #endregion
        
        #region SerializeFields
        
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Vector3 _moveDirection;
        [SerializeField] private Transform _rightEdge;
        [SerializeField] private Transform _leftEdge;
        [SerializeField] private SpriteRenderer _cameraWidth;
        
        #endregion
        
        #region SpriteRenderers
        
        private SpriteRenderer _leftEdgeSprite;
        private SpriteRenderer _rightEdgeSprite;
        
        #endregion

        #region MVC: Init
        
        public override void Init()
        {
            _leftEdgeSprite = _leftEdge.GetComponent<SpriteRenderer>();
            _rightEdgeSprite = _rightEdge.GetComponent<SpriteRenderer>();
        }

        public override void DeInit()
        {
            _leftEdgeSprite = null;
            _rightEdgeSprite = null;
        }

        #endregion
        
        #region UnityMethods: Update
        
        private void Update()
        {
           
            if ((Model.CamSpeed < Model.CameraMinDeadZone  || Model.CamSpeed > Model.CameraMaxDeadZone) && GameManager.Instance.State == GameState.Initial)
            {
                CameraMovement();
            }
        }
        
        #endregion
        
        #region Method: CameraMovement
        
        private void CameraMovement()
        {
            var minEdge = _leftEdge.position.x -_leftEdgeSprite.bounds.size.x/2+ _cameraWidth.bounds.size.x/2 ;
            var maxEdge = _rightEdge.position.x + _rightEdgeSprite.bounds.size.x/2 - _cameraWidth.bounds.size.x/2;
            mainCamera.transform.localPosition = new Vector3(Mathf.Clamp( mainCamera.transform.localPosition.x, minEdge, maxEdge), mainCamera.transform.position.y, mainCamera.transform.position.z);
            mainCamera.transform.Translate((_moveDirection * (Time.deltaTime * -Model.CamSpeed/6)),Space.Self);
        }
        
        #endregion
    }
}