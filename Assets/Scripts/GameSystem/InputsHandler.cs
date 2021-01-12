using GameSystem.GameSceneManagement;
using System;
using UnityEngine;
using UnityEngine.InputSystem;


namespace GameSystem.InputSystem
{
    public class InputsHandler : MonoBehaviour
    {
        public static InputsHandler Instance { get; private set; } 

        private static PlayerInput input;


        private void Awake()
        {
            CreateInstance();

            input = GetComponent<PlayerInput>();
        }

        private void OnEnable()
        {
            EnableInputs();

            if (!GameManager.Instance) return;

            GameManager.OnGamePause += EnableMouse;
            GameManager.OnGameResume += DisableMouse;
        }

        private void OnDisable()
        {
            DisableInputs();

            if (!GameManager.Instance) return;

            GameManager.OnGamePause -= EnableMouse;
            GameManager.OnGameResume -= DisableMouse;
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        public static void EnableInputs()
        {
            input.enabled = true;
        }

        public static void DisableInputs()
        {
            input.enabled = false;
        }

        public void Move(InputAction.CallbackContext context)
        {
            //Debug.Log(GameManager.PlayState);
            //if (GameManager.PlayState != GameplayState.InProgress) return;

            OnMove?.Invoke(context.ReadValue<Vector2>(), context.phase);
        }

        public void Focus(InputAction.CallbackContext context)
        {
            if (GameManager.PlayState != GameplayState.InProgress) return;

            OnFocus?.Invoke(context.phase);
        }

        public void FireGun(InputAction.CallbackContext context)
        {
            if (GameManager.PlayState != GameplayState.InProgress) return;

            OnFireGun?.Invoke(context.phase);
        }

        public void FireMissiles(InputAction.CallbackContext context)
        {
            if (GameManager.PlayState != GameplayState.InProgress) return;

            OnFireMissiles?.Invoke(context.phase);
        }

        public void UseUltimate(InputAction.CallbackContext context)
        {
            if (GameManager.PlayState != GameplayState.InProgress) return;

            OnUseUltimate?.Invoke(context.phase);
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (Credits.Instance)
            {
                Credits.Instance.LoadMainMenu();
                return;
            }

            if ((!InGameMenuManager.Instance && !LevelManager.Instance) 
               // || GameManager.PlayState != GameplayState.InProgress
                || SceneTransition.Instance.Anim.isPlaying) return;

            InGameMenuManager.Instance.TogglePauseMenu();
        }

        private void CreateInstance()
        {
            if (!Instance) Instance = this;
            else Destroy(this.gameObject);
        }

        public static void EnableMouse()
        {
            Cursor.visible = true;
        }

        public static void DisableMouse()
        {
            Cursor.visible = false;
        }

        public void OnSceneStarted()
        {
            if (LevelManager.Instance) DisableMouse();
            else EnableMouse();
        }


        public static event Action<InputActionPhase> OnFocus;
        public static event Action<InputActionPhase> OnFireMissiles;
        public static event Action<Vector2, InputActionPhase> OnMove;
        public static event Action<InputActionPhase> OnFireGun;
        public static event Action<InputActionPhase> OnUseUltimate;
    }
}
