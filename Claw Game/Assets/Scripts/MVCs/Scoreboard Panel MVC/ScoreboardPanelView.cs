using System.Collections;
using TMPro;
using UnityEngine;
using Minigame.Data;
namespace Minigame.Presentation
{
    public class ScoreboardPanelView : BaseView<ScoreboardPanelModel>
    {
        #region SerializeField
        
        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _goldAmountText;
        [SerializeField] private TextMeshProUGUI _gemAmountText;
        
        #endregion
    
        #region Private Variables
        
        private int _gemDisplayScore;
        private int _goldDisplayScore;
        
        #endregion
    
        #region MVC: Init | InitTexts
        
        public override void Init()
        {
            StartCoroutine(ScoreUpdater());
            InitTexts();
        }

        public override void DeInit()
        {
            StopCoroutine(ScoreUpdater());
        }

        private void InitTexts()
        {
            _gemAmountText.text = Model.GemAmount.ToString();
            _goldAmountText.text = Model.GoldAmount.ToString();
        }
        
        #endregion

        #region Event: OnVerticalClawMoveComplete
        
        public void OnClawVerticalClawVerticalMoveComplete(object sender, ClawMoveCompleteArgs e)
        {
            foreach (var collectible in e.collectedRewardList)
            {
                switch (collectible.RewardType.type)
                {
                    case RewardTypes.Gem:
                        Model.GemAmount += collectible.RewardType.amount;
                        break;
                    case RewardTypes.Gold:
                        Model.GoldAmount += collectible.RewardType.amount;
                        break;
                }
            }
            e.collectedRewardList.Clear();
        }
        
        #endregion
    
        #region Coroutine: ScoreUpdater
        
        private IEnumerator ScoreUpdater()
        {
            while (true)
            {
                if (_gemDisplayScore < Model.GemAmount)
                {
                    _gemDisplayScore++;
                    _gemAmountText.text = _gemDisplayScore.ToString();
                
                }
                if (_goldDisplayScore < Model.GoldAmount)
                {
                    _goldDisplayScore++;
                    _goldAmountText.text = _goldDisplayScore.ToString();
                
                }

                yield return new WaitForSeconds(0.007f);
            }
        }
        
        #endregion
    }
}