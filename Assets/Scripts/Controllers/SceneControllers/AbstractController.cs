using Models;
using UnityEngine;

namespace Controllers.SceneControllers
{
    public abstract class AbstractController : MonoBehaviour
    {
        private ControllerModel _model;

        protected int CoinCount
        {
            get => _model.CoinsCount;
            set => _model.CoinsCount = value;
        }

        protected int TurnOnMusic
        {
            get => _model.TurnOnMusic;
            set => _model.TurnOnMusic = value;
        }

        protected int TurnOnSound
        {
            get => _model.TurnOnSound;
            set => _model.TurnOnSound = value;
        }

        private void OnEnable()
        {
            _model = new ControllerModel();
            
            OnEnableScene();
        }

        private void Start()
        {
            OnStartScene();
        }

        private void OnDisable()
        {
            OnDisableScene();
        }

        protected abstract void OnEnableScene();

        protected abstract void OnStartScene();

        protected abstract void OnDisableScene();
    }
}
