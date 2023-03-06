using System;
using UnityEngine;
using Minigame.Data;
namespace Minigame.Presentation
{
    public class Reward : MonoBehaviour
    {
        #region SerializeField
        
        [SerializeField] private RewardType _rewardType;
        public RewardType RewardType => _rewardType;
        
        #endregion

        #region Method: Init
        
        public void Init()
        {
            _rewardType = Resources.Load<RewardType>("Scriptable Objects/Collectible Data/"+ gameObject.name);
        }
        
        #endregion

    }
}