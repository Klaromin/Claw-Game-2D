using System;
using DG.Tweening;
using UnityEngine;
using Minigame.Data;

namespace Minigame.Presentation
{
    public class GameplayView : BaseView<GameplayModel>
    {
    
        #region SerializeFields
        [SerializeField] private ClawView _clawView;
        public ClawView ClawView => _clawView;

        [SerializeField] private Reward _smallGold;
        [SerializeField] private Reward _mediumGold;
        [SerializeField] private Reward _largeGold;
        [SerializeField] private Reward _smallGem;
        [SerializeField] private Reward _mediumGem;
        [SerializeField] private Reward _largeGem;
        [SerializeField] private GameObject _spawnLocation;
        [SerializeField] private Transform _rightBoundary;
        [SerializeField] private Transform _gemEndPoint;
        [SerializeField] private Transform _goldEndPoint;
        #endregion

        #region MVC: Init | DeInit
        public override void Init()
        { 
            Model.GameData = Resources.Load<GameData>("Scriptable Objects/Game Data/Game Data");
            InitRewards();
        }

        public override void DeInit()
        {
            Model.GameData = null;
        }

        #endregion
    
        #region Method: InitRewards
        private void InitRewards()
        {
            _smallGem.Init();
            _mediumGem.Init();
            _largeGem.Init();
            _smallGold.Init();
            _mediumGold.Init();
            _largeGold.Init();
            Vector3 nextSpawn = new Vector3(0,0,0);
            var boundary = _rightBoundary.position.x - _spawnLocation.transform.position.x;
            float smallRewardYOffset = 0.38f;
            float mediumRewardYOffset = 0.4f;
            var largeRewardYOffset = 1.5f;
            var batch1 = Math.Max(Model.GameData.largeGemSpawnAmount, Model.GameData.largeGoldSpawnAmount);
            var batch2 = Math.Max(Model.GameData.mediumGoldSpawnAmount, Model.GameData.mediumGemSpawnAmount);
            var batch3 = Math.Max(Model.GameData.smallGemSpawnAmount, Model.GameData.smallGoldSpawnAmount);
            for (int i = 0; i < batch3; i++)
            {
            
                if (nextSpawn.x > boundary)
                {
                    nextSpawn.y -= smallRewardYOffset;
                    nextSpawn.x = 0;
                }
                nextSpawn.x += 0.3f;
                if (i <= Model.GameData.smallGoldSpawnAmount)
                {
                    Instantiate(_smallGold, _spawnLocation.transform.position + nextSpawn , Quaternion.identity, _spawnLocation.transform);
                }
                if (i <= Model.GameData.smallGemSpawnAmount )
                {
                    Instantiate(_smallGem, _spawnLocation.transform.position + nextSpawn, Quaternion.identity, _spawnLocation.transform);
                }
            }

            for (int i = 0; i < batch2; i++)
            {
                if (nextSpawn.x > boundary)
                {
                    nextSpawn.y -= mediumRewardYOffset;
                    nextSpawn.x = 0;
                }
                nextSpawn.x += 0.5f;
                if (i <= Model.GameData.mediumGemSpawnAmount )
                {
                    Instantiate(_mediumGem, _spawnLocation.transform.position + nextSpawn , Quaternion.identity, _spawnLocation.transform);
                }
                if (i <= Model.GameData.mediumGoldSpawnAmount)
                {
                    Instantiate(_mediumGold, _spawnLocation.transform.position + nextSpawn , Quaternion.identity, _spawnLocation.transform);
                }
            }
            for (int i = 0; i < batch1; i++)
            {
                if (nextSpawn.x > boundary)
                {
                    nextSpawn.y -= largeRewardYOffset;
                    nextSpawn.x = 0;
                }
                nextSpawn.x += 0.8f;
           
                if (i <= Model.GameData.largeGemSpawnAmount )
                {
                    Instantiate(_largeGem,_spawnLocation.transform.position + nextSpawn , Quaternion.identity, _spawnLocation.transform);
                }
                if (i <= Model.GameData.largeGoldSpawnAmount )
                {
                    Instantiate(_largeGold, _spawnLocation.transform.position + nextSpawn ,Quaternion.identity, _spawnLocation.transform);
                }
            }
        }
        #endregion
    
        #region Event: OnVerticalClawMovementComplete
        public void OnVerticalClawMovementComplete(object sender, ClawMoveCompleteArgs e)
        {
        
            if (GameManager.Instance.State == GameState.Decision)
            {
                float duration = 0.5f;
                float nextDurationAddition = 0;
                foreach (var reward in e.collectedRewardList) //listeyi gezerken listenin kendisini silemem. for ile dön foreach ile dönmeden array'e çevir. datastructure çalış cs50 freecodecamp
                {
                
                    RewardMovement(reward, duration+ nextDurationAddition);
                    nextDurationAddition += 0.2f;
               
                }
            }
        }
        #endregion
    
        #region Method: RewardMovement
        private void RewardMovement(Reward reward, float duration)
        {
            if(reward.RewardType.type == RewardTypes.Gem)
                reward.gameObject.transform.DOMove(_gemEndPoint.position, duration).SetEase(Ease.Flash).OnComplete(() => Destroy(reward.gameObject));
            else
            {
                reward.gameObject.transform.DOMove(_goldEndPoint.position, duration).SetEase(Ease.Flash).OnComplete(() => Destroy(reward.gameObject));
            }
        }
        
        #endregion
    
    }
}