using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Controllers.Game;
using Models.Game;
using Views.Game;

namespace Controllers.SceneControllers
{
    public class GameController : AbstractController
    {
        [Space(5)] [Header("BoardPrefabs")]
        [SerializeField] 
        private List<GameObject> _boardsPrefabs;
        [Space(5)] [Header("RectTransforms")]
        [SerializeField] 
        private RectTransform _gameBoardRect;
        [SerializeField] 
        private RectTransform _swappingOverlayRectTransform;
        [Space(5)] [Header("Views")] 
        [SerializeField]
        private UpdateTextView _levelCounterView;
        [SerializeField] 
        private UpdateTextView _targetView;
        [SerializeField] 
        private UpdateTextView _remainderTargetView;
        [SerializeField] 
        private TargetPanelView _targetPanelView;
        [SerializeField] 
        private BoosterZoneView _boosterZoneView;
        [Space(5)] [Header("Buttons")] 
        [SerializeField]
        private Button _destroySameCellsBtn;
        [SerializeField] 
        private Button _destroyOneColumnBtn;
        [SerializeField] 
        private Button _backBtn;

        private Board _boardController;
        private GameModel _model;

        private int _targetScore;
        private int _currentScore;

        private bool _isGaming;
        
        protected override void OnEnableScene()
        {
            _model = new GameModel();
            
            OpenTargetPanel();

            _currentScore = 0;
            
            _destroySameCellsBtn.onClick.AddListener(DestroySameCells);
            _destroyOneColumnBtn.onClick.AddListener(DestroyOneColumn);
            _backBtn.onClick.AddListener(delegate { LoadScene("Menu"); });
        }

        protected override void OnStartScene()
        {
            
        }

        protected override void OnDisableScene()
        {
            _destroySameCellsBtn.onClick.RemoveAllListeners();
            _destroyOneColumnBtn.onClick.RemoveAllListeners();
            _backBtn.onClick.RemoveAllListeners();
        }

        private void StartGame()
        {
            int level = _model.CurrentLevel;
            _targetScore = level * 100;
            
            _targetView.UpdateText(_targetScore);
            _levelCounterView.UpdateText(level);
            UpdateRemainderTarget(-_targetScore);
            CheckCountBooster();
            
            var index = Random.Range(0, _boardsPrefabs.Count - 1);

            GameObject go = Instantiate(_boardsPrefabs[index], _gameBoardRect.transform);
            go.GetComponent<RectTransform>().SetSiblingIndex(0);
            _boardController = go.GetComponent<Board>();
            
            _boardController.SetSwappingOverlayTransform(_swappingOverlayRectTransform);
            _boardController.CellsSwappedAction += AddPoints;

            _isGaming = true;
            
            StartCoroutine(Timer());
        }

        private void AddPoints(int points)
        {
            switch (points)
            {
                case 0:
                    _currentScore += 20;
                    break;
                case 1:
                    _currentScore += 100;
                    break;
                case 2:
                    _currentScore += 50;
                    break;
            }
            
            Debug.Log("AddPoints");
            
            var remainderTarget = _model.RemainderTarget(_currentScore);
            UpdateRemainderTarget(remainderTarget);
            
            Debug.Log(remainderTarget >= 0 ? "Win" : "Continue");

            if (remainderTarget >= 0)
            {
                _isGaming = false;
            }
        }

        private void UpdateRemainderTarget(int remainderTarget)
        {
            _remainderTargetView.UpdateText(remainderTarget);
        }

        private void OpenTargetPanel()
        {
            _targetPanelView.gameObject.SetActive(true);
            
            _targetPanelView.UpdateTexts(_model.CurrentLevel, _model.CurrentLevel * 100, _model.SameCellsBoosterCount, _model.OneColumnBoosterCount);

            _targetPanelView.PressBtnAction += CheckResultTargetPanel;
        }

        private void CheckResultTargetPanel(int result)
        {
            switch (result)
            {
                case 0 :
                    _targetPanelView.gameObject.SetActive(false);
                    StartGame();
                    break;
                case 1:
                    LoadScene("Menu");
                    break;
            }
        }

        private void DestroySameCells()
        {
            if (_model.SameCellsBoosterCount > 0)
            {
                _boardController.ChangeCanDestroySameCells();
                _model.SameCellsBoosterCount--;
                CheckCountBooster();
            }
        }

        private void DestroyOneColumn()
        {
            if (_model.OneColumnBoosterCount > 0)
            {
                _boardController.ChangeCanDestroyOneColumn();
                _model.OneColumnBoosterCount--;
                CheckCountBooster();
            }
        }

        private void CheckCountBooster()
        {
            _boosterZoneView.UpdateFirstBoosterCountText(_model.SameCellsBoosterCount);
            _boosterZoneView.UpdateSecondBoosterCountText(_model.OneColumnBoosterCount);

            _boosterZoneView.SetActiveBoosters(0, _model.SameCellsBoosterCount > 0);
            _boosterZoneView.SetActiveBoosters(1, _model.OneColumnBoosterCount > 0);
        }

        private void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        private IEnumerator Timer()
        {
            int seconds = 0;

            while (_isGaming)
            {
                yield return new WaitForSeconds(1);
                seconds++;
            }
            
            Debug.Log(seconds);
            _model.TotalSeconds = seconds;

            _model.CurrentLevel++;
            
            LoadScene("Result");
        }
    }
}
