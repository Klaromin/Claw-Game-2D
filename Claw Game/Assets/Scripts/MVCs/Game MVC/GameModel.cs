namespace Minigame.Presentation
{
    public class GameModel : BaseModel
    {
        public float CamSpeed { get; set; }
        
        #region Camera DeadZone

        public int CameraMaxDeadZone => 10;
        public int CameraMinDeadZone => -10;

        #endregion
    }
}