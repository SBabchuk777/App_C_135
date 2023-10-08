using System.Collections.Generic;
using UnityEngine;
using Controllers.Game;

namespace Controllers.SceneControllers
{
    public class GameController : AbstractController
    {
        [Space(5)]
        [Header("BoardPrefabs")]
        [SerializeField] 
        private List<GameObject> _boardsPrefabs;
        [Space(5)]
        [Header("RectTransforms")]
        [SerializeField] 
        private RectTransform _gameBoardRect;
        [SerializeField] 
        private RectTransform _swappingOverlayRectTransform;

        private Board _boardController;

        protected override void OnEnableScene()
        {
            StartGame();
        }

        protected override void OnStartScene()
        {
            
        }

        protected override void OnDisableScene()
        {
            
        }

        private void StartGame()
        {
            var index = Random.Range(0, _boardsPrefabs.Count - 1);

            GameObject go = Instantiate(_boardsPrefabs[index], _gameBoardRect.transform);
            go.GetComponent<RectTransform>().SetSiblingIndex(0);
            _boardController = go.GetComponent<Board>();
            
            _boardController.SetSwappingOverlayTransform(_swappingOverlayRectTransform);
            _boardController.CellsSwappedAction += AddPoints;
        }

        private void AddPoints()
        {
            Debug.Log("AddPoints");
        }
    }
}
