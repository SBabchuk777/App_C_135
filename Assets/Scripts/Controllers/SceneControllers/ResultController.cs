using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Models.Game;

namespace Controllers.SceneControllers
{
    public class ResultController : AbstractController
    {
        [Space(5)]
        [Header("Texts")]
        [SerializeField] 
        private Text _coinsCountText;
        [SerializeField] 
        private Text _scoreText;
        [SerializeField] 
        private Text _timeText;
        [Space(5)] [Header("Buttons")] 
        [SerializeField]
        private Button _closeBtn;
        [SerializeField] 
        private Button _nextLevelBtn;

        private GameModel _model;

        protected override void OnEnableScene()
        {
            _model = new GameModel();
            
            UpdateTexts();
            
            _closeBtn.onClick.AddListener(delegate { LoadScene("Menu"); });
            _nextLevelBtn.onClick.AddListener(delegate { LoadScene("Game"); });
        }

        protected override void OnStartScene()
        {
            
        }

        protected override void OnDisableScene()
        {
            _closeBtn.onClick.RemoveAllListeners();
            _nextLevelBtn.onClick.RemoveAllListeners();
        }

        private void UpdateTexts()
        {
            _coinsCountText.text = CoinCount.ToString();
            _scoreText.text = "Scored: " + (_model.CurrentLevel - 1) * 100 + " EX";

            TimeSpan time = TimeSpan.FromSeconds(_model.TotalSeconds);

            _timeText.text = "Time: " + time.Minutes + " min " + time.Seconds + "sec";
        }

        private void LoadScene(string nameScene)
        {
            SceneManager.LoadScene(nameScene);
        }
    }
}
