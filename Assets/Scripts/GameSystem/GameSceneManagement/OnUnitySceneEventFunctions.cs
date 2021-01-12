using GameSystem.InputSystem;
using GameSystem.Weapons.Projectiles;
using System;
using UnityEngine;


namespace GameSystem.GameSceneManagement
{
    /// <summary>
    /// Mainly used for calls to the GameManager.
    /// 
    /// Checks for Start, Destroyed states.
    /// </summary>
    public class OnUnitySceneEventFunctions : MonoBehaviour
    {
        public static OnUnitySceneEventFunctions Instance { private set; get; }
       // private bool appIsQuitting = false; //Prevents Invoke when App is quitting.

        private void Awake()
        {
            if (!Instance) Instance = this;
            else
            {
                Destroy(Instance.gameObject);
                Instance = this;
            }
        }

        private void OnEnable()
        {
            if (!GameManager.Instance) return;
            AddSubscribers();
        }

        private void Start()
        {
            OnSceneStarted?.Invoke();

            Array.ForEach(GameObject.FindObjectsOfType<Projectile>(), p => { if (p.gameObject.activeSelf) p.DisableObj(); });
        }

        private void OnDestroy()
        {
            if ( !GameManager.Instance) return;//appIsQuitting ||

            OnSceneDestroyed?.Invoke();
            RemoveSubscribers();
            Instance = null;
        }

        //private void OnApplicationQuit()
        //{
        //    appIsQuitting = true;
        //}

        private void AddSubscribers()
        {
            OnSceneStarted += GameManager.Instance.OnSceneStarted;
            OnSceneStarted += GameSceneLoader.Instance.OnSceneStarted;
            OnSceneStarted += InputsHandler.EnableInputs;
            OnSceneStarted += InputsHandler.Instance.OnSceneStarted;
            OnSceneStarted += MusicManager.Instance.IncreaseVolume;

            OnSceneDestroyed += InputsHandler.DisableInputs;
            OnSceneDestroyed += LevelUIManager.Instance.OnSceneDestroyed;
        }

        private void RemoveSubscribers()
        {
            OnSceneStarted -= GameManager.Instance.OnSceneStarted;
            OnSceneStarted -= InputsHandler.EnableInputs;
            OnSceneStarted -= GameSceneLoader.Instance.OnSceneStarted;
            OnSceneStarted -= InputsHandler.Instance.OnSceneStarted;
            OnSceneStarted -= MusicManager.Instance.IncreaseVolume;

            OnSceneDestroyed -= InputsHandler.DisableInputs;
            OnSceneDestroyed -= LevelUIManager.Instance.OnSceneDestroyed;
        }

        public event Action OnSceneStarted;
        public event Action OnSceneDestroyed;
    }
}
