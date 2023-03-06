using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame.Data
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Reward Type", fileName = " New Reward Type")]
    public class RewardType : ScriptableObject
    {
        public int amount;
        public RewardTypes type;
        public RewardSize size;

    }

    public enum RewardTypes
    {
        Gold,
        Gem
    }

    public enum RewardSize
    {
        Small,
        Medium,
        Large
    }
}

