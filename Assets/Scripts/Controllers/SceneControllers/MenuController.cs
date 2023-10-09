using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Controllers.SceneControllers
{
    public class MenuController : AbstractController
    {
        [Space(5)] [Header("Buttons")] 
        [SerializeField]
        private Button _openSettingsBtn;
        [SerializeField]
        private Button _openShopBtn;
        [SerializeField] 
        private Button _openGameBtn;
        [Space(5)] [Header("Texts")] 
        [SerializeField]
        private Text _coinCountText;
        
        protected override void OnEnableScene()
        {
            UpdateCoinText();
            
            _openGameBtn.onClick.AddListener(delegate { LoadScene("Game"); });
            _openSettingsBtn.onClick.AddListener(delegate { LoadScene("Settings"); });
            _openShopBtn.onClick.AddListener(delegate { LoadScene("Shop"); });
        }

        protected override void OnStartScene()
        {
            
        }

        protected override void OnDisableScene()
        {
            _openGameBtn.onClick.RemoveAllListeners();
            _openSettingsBtn.onClick.RemoveAllListeners();
            _openShopBtn.onClick.RemoveAllListeners();
        }

        private void UpdateCoinText()
        {
            _coinCountText.text = CoinCount.ToString();
        }

        private void LoadScene(string nameScene)
        {
            SceneManager.LoadScene(nameScene);
        }
    }
}
