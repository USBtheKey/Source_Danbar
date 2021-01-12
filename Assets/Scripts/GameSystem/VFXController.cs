using GameSystem.GameSceneManagement;
using System.Collections;
using UnityEngine;



namespace GameSystem
{
    public abstract class VFXController : MonoBehaviour, ISceneObject
    {
        protected WaitForSeconds waitForSeconds;

        protected virtual void Awake()
        {
            //Empty
        }

        private void OnEnable()
        {
            if (LevelManager.Instance == null) return;

            StartCoroutine(Play_Routine());
        }

        private void Start()
        {
            GameSceneLoader.OnStartLoadingNextScene += DisableObj;
        }

        public void DisableObj()
        {
            gameObject.SetActive(false);
        }

        protected abstract IEnumerator Play_Routine();
    }
}
