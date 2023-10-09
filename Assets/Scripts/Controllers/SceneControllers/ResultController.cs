using System;
using System.Collections;
using System.Collections.Generic;
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
        [Space(5)] [Header("AudioClips")] 
        [SerializeField]
        private AudioClip _clickClip;
        [SerializeField] 
        private AudioClip _winClip;

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
            PlaySound(_winClip);
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
            PlaySound(_clickClip);

            StartCoroutine(DelayLoadScene(nameScene));
        }

        private IEnumerator DelayLoadScene(string nameScene)
        {
            yield return new WaitForSeconds(0.3f);
            
            SceneManager.LoadScene(nameScene);
        }
    }
}
