using UnityEngine;

namespace Controllers.SceneControllers
{
    public abstract class AbstractController : MonoBehaviour
    {
        private void OnEnable()
        {
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
