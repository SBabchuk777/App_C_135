using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Views.Bonus;
using Models;

namespace Controllers.SceneControllers
{
    public class BonusController : AbstractController
    {
        [Space(5)] [Header("Views")] 
        [SerializeField] 
        private PrizeZoneView _prizeZoneView;
        [SerializeField] 
        private WheelView _wheelView;
        [Space(5)] [Header("UI")] 
        [SerializeField]
        private Text _coinCountText;
        [SerializeField] 
        private Button _spinBtn;
        [SerializeField] 
        private Button _closeBtn;
        [Space(5)] [Header("AudioClips")] 
        [SerializeField]
        private AudioClip _clickClip;
        [SerializeField] 
        private AudioClip _rotationWheelClip;

        private BonusModel _model;

        protected override void OnEnableScene()
        {
            _model = new BonusModel();
            
            CheckLastDay();
            
            UpdateCoinCountText();
            
            _closeBtn.onClick.AddListener(LoadSceneMenu);
            _spinBtn.onClick.AddListener(StartAnim);
        }

        protected override void OnStartScene()
        {
            
        }

        protected override void OnDisableScene()
        {
            _closeBtn.onClick.RemoveAllListeners();
            _spinBtn.onClick.RemoveAllListeners();
        }

        private void UpdateCoinCountText()
        {
            _coinCountText.text = CoinCount.ToString();
        }

        private void StartAnim()
        {
            _spinBtn.interactable = false;
            _wheelView.StartRotateWheel();
            _wheelView.RotationEndAction += EndRotation;
        }

        private void EndRotation(int value)
        {
            if (value == -1)
            {
                _spinBtn.interactable = true;
            }
            else
            {
                CoinCount += value;
                _prizeZoneView.gameObject.SetActive(true);
                _prizeZoneView.UpdateText(value);
                UpdateCoinCountText();
            }
        }

        private void LoadSceneMenu()
        {
            _model.LastDayOpen = DateTime.Now.Day;
            
            SceneManager.LoadScene("Menu");
        }

        private void CheckLastDay()
        {
            if (!_model.CanRotateWheel)
            {
                LoadSceneMenu();
            }
        }
    }
}