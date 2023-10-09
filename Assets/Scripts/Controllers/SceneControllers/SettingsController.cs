using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Views.Settings;

namespace Controllers.SceneControllers
{
    public class SettingsController : AbstractController
    {
        [Space(5)] [Header("Views")]
        [SerializeField] 
        private SwitcherView _musicSwitcherView;
        [SerializeField] 
        private SwitcherView _soundSwitcherView;
        [SerializeField] 
        private Text _coinsCountText;
        [Space(5)] [Header("Buttons")]
        [SerializeField] 
        private Button _switcherMusicBtn;
        [SerializeField] 
        private Button _switcherSoundBtn;
        [SerializeField] 
        private Button _closeBtn;
        [SerializeField] 
        private Button _ppBtn;
        [SerializeField] 
        private Button _termsBtn;

        protected override void OnEnableScene()
        {
            _switcherSoundBtn.onClick.AddListener(ChangeSoundTurn);
            _switcherMusicBtn.onClick.AddListener(ChangeMusicTurn);
            _ppBtn.onClick.AddListener(SetClickSound);
            _termsBtn.onClick.AddListener(SetClickSound);
            _closeBtn.onClick.AddListener(LoadSceneMenu);
            
            SetSpriteSoundSwitcher();
            SetSpriteMusicSwitcher();

            _coinsCountText.text = CoinCount.ToString();
        }

        protected override void OnStartScene()
        {
            
        }

        protected override void OnDisableScene()
        {
            _switcherSoundBtn.onClick.RemoveAllListeners();
            _switcherMusicBtn.onClick.RemoveAllListeners();
            _ppBtn.onClick.RemoveAllListeners();
            _termsBtn.onClick.RemoveAllListeners();
            _closeBtn.onClick.RemoveAllListeners();
        }

        private void SetSpriteSoundSwitcher()
        {
            _soundSwitcherView.UpdateSwitcherSprite(TurnOnSound);
        }
        
        private void SetSpriteMusicSwitcher()
        {
            _musicSwitcherView.UpdateSwitcherSprite(TurnOnMusic);
        }

        private void ChangeSoundTurn()
        {
            TurnOnSound = TurnOnSound == 0 ? 1 : 0;
            
            SetSpriteSoundSwitcher();
        }

        private void ChangeMusicTurn()
        {
            TurnOnMusic = TurnOnMusic == 0 ? 1 : 0;
            
            SetSpriteMusicSwitcher();
        }

        private void SetClickSound()
        {
            
        }

        private void LoadSceneMenu()
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
