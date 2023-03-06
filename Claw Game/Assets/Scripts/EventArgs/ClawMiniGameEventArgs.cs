using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Minigame.Presentation;
namespace Minigame.Data
{
    public class ClawMiniGameEventArgs
    {

    }

    public class ClawMoveCompleteArgs : EventArgs//Eventargsları farklı class'ta yarat
    {
        public List<Reward> collectedRewardList;
    }

    public class StickMovementArgs : EventArgs
    {
        public float speedMultiplier;
    }
}


