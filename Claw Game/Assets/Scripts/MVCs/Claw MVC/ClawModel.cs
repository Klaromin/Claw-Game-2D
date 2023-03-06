using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame.Presentation
{
    public class ClawModel : BaseModel
    {
        #region Speeds

        public float ClawSpeed { get; set; }

        #endregion
        
        #region Locations
        
        public float ClawMaxReachPoint { get; set; }
        public float ClawMinReachPoint { get; set; }
        
        #endregion
        
        #region Durations
        internal float ClawDiveDuration => 2f;
        internal float ClawOpeningDuration => 0.5f;
        internal float ClawClosingDuration => 2.8f;
        internal float ClawReleaseDuration => 1f;
        
        #endregion
        
        #region DeadZone Configs
        
        public readonly int ClawMinDeadZone =-10;
        public readonly int ClawMaxDeadZone =10;
        
        #endregion
        
        #region Degrees
        
        public readonly float ClawOpeningDegree = 30;
        public readonly float ClawClosingDegree = 15;
        public readonly float ClawDiveReachPoint = -2.6f;
        
        #endregion
    }  
}