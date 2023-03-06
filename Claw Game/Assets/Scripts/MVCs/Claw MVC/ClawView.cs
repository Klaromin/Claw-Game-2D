using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using Minigame.Data;

namespace Minigame.Presentation
{
    public class ClawView : BaseView<ClawModel>
    {
        #region SerializeFields
  
        [SerializeField] private GameObject _leftClaw;
        [SerializeField] private GameObject _rightClaw;
        [SerializeField] private GameObject _clawHolder;
        [SerializeField] private Vector2 _moveDirection;
        [SerializeField] private Transform _leftEdge;
        [SerializeField] private Transform _rightEdge;
        [SerializeField] private SpriteRenderer _clawSprite;
        [SerializeField] private List<Reward> _catchedRewards;
        [SerializeField] private List<Reward> _largeRewardList;
 
        #endregion
 
        #region Private Variables
        
        private Collider2D _rewardDetector;
        private int _largeSizeReward;

        #endregion
 
        #region EventHandlers
        
        public event EventHandler<ClawMoveCompleteArgs> OnVerticalClawMovementCompleteEvent;
        
        #endregion

        #region MVC: Init | Deinit | InitProperties
        
        public override void Init()
        {
            AddEvents();
            InitProperties();
        }

        public override void DeInit()
        {
            RemoveEvents();
        }

        private void InitProperties()
        {
            _rewardDetector = GetComponent<Collider2D>();
            Model.ClawMaxReachPoint = _rightEdge.position.x - _clawSprite.bounds.size.x;
            Model.ClawMinReachPoint = _leftEdge.position.x + _clawSprite.bounds.size.x;
            _largeSizeReward = 0;
        }
        
        #endregion

        #region Unity: Update | OnDisable
        
        public void Update()
        {
            Debug.Log(_largeSizeReward);
            // if (GameManager.Instance.State == GameState.Movement)
            // {
            //     LargeRewardDetection();
            // }
            
            if ((Model.ClawSpeed <Model.ClawMinDeadZone || Model.ClawSpeed> Model.ClawMaxDeadZone) && GameManager.Instance.State == GameState.Initial)
            {
                Move();
            }
            _rewardDetector.enabled = GameManager.Instance.State == GameState.Movement;
        }

        #endregion

        #region Movement: Move | Dive | Release
        
        private void Move()
        {
            transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, Model.ClawMinReachPoint, Model.ClawMaxReachPoint), transform.localPosition.y, transform.localPosition.z);
            transform.Translate(_moveDirection * (Time.deltaTime * -Model.ClawSpeed/6), Space.Self);
        }

        public void Dive()
        {
            _clawHolder.transform.DOLocalMoveY(Model.ClawDiveReachPoint, Model.ClawDiveDuration).SetLoops(2, LoopType.Yoyo);
           
            _rightClaw.transform.DOLocalRotate(new Vector3(0,0,Model.ClawOpeningDegree), Model.ClawOpeningDuration).
                OnComplete(()=>_rightClaw.transform.DOLocalRotate(new Vector3(0, 0, -Model.ClawClosingDegree), Model.ClawClosingDuration)) ;
            
            _leftClaw.transform.DOLocalRotate(new Vector3(0, 0, -Model.ClawOpeningDegree), Model.ClawOpeningDuration).
                OnComplete(()=>_leftClaw.transform.DOLocalRotate(new Vector3(0, 0, Model.ClawClosingDegree), Model.ClawClosingDuration));
        }

        private void Release()
        {
            _rightClaw.transform.DOLocalRotate(new Vector3(0, 0, Model.ClawClosingDegree), Model.ClawReleaseDuration);
            _leftClaw.transform.DOLocalRotate(new Vector3(0, 0, -Model.ClawClosingDegree), Model.ClawReleaseDuration);
        }
        
        #endregion
 
        #region Collider: OnTriggerEnter2D
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag != "Collectible") return;
            var a = col.gameObject.GetComponent<Reward>();
            a.gameObject.layer = 6;
            // if (a.RewardType.size == RewardSize.Large)
            // {
            //     _largeSizeReward++;
            // }

            _catchedRewards.Add(a);
            LargeRewardDetection();
            if (_largeRewardList.Count > 1 && a.RewardType.size == RewardSize.Large)
            {
                _catchedRewards.Remove(a);
            }
            // LargeRewardDetection();
            // _largeSizeReward = 0;
        }
        
        #endregion

        private void LargeRewardDetection()
        {
            
            _largeRewardList = _catchedRewards.Where(a => a.RewardType.size == RewardSize.Large).ToList();
            // Debug.Log(_largeRewardList.Count);
            // foreach (var reward in _largeRewardList)
            // {
            //     
            //     if (_largeRewardList.Count > 1)
            //     {
            //         _catchedRewards.
            //     }
            //
            // }
            // _largeRewardList.Clear();
            

            // for (int i = 0; i < largeRewardList.Count-1; i++)
            // {
            //     
            // }
            // if (_largeSizeReward <= 1) return;
            //
            // if (a.RewardType.size == RewardSize.Large)
            // {
            //     col.gameObject.layer = 7;
            //     _catchedRewards.Remove(a);
            // }
                
            
        }
        
        #region Event: OnVerticalClawMoveCompleteEvent
        
        private void OnVerticalClawMovementComplete()
        {
            OnVerticalClawMovementCompleteEvent?.Invoke(this, new ClawMoveCompleteArgs()
            {
                collectedRewardList =
                    _catchedRewards 
            });
        }
        
        #endregion

        #region Event: OnGameStateChanged
        
        private void OnGameStateChanged(GameState state)
        {
            if (state != GameState.Decision) return;
            OnVerticalClawMovementComplete();
            Release();
        }
        
        #endregion
 
        #region Events: Add | Remove
        
        private void AddEvents()
        {
            GameManager.GameStateChangedEvent += OnGameStateChanged;
        }
 
        private void RemoveEvents()
        {
            GameManager.GameStateChangedEvent -= OnGameStateChanged;
        }
        
        #endregion
    }
}