using System.Collections;
using GameSystem.GameSceneManagement;
using UnityEngine;


namespace GameSystem
{
    public class PlayerLockingBG_VFX : MonoBehaviour, ISceneDestroyed
    {
        private SpriteRenderer sRend;

        public static PlayerLockingBG_VFX Instance { get; private set; }
        private float activeTime = 0.2f;
        private void Awake()
        {
            if (!Instance) Instance = this;
            else Destroy(gameObject);

            sRend = GetComponent<SpriteRenderer>();
        }

        public void Play()
        {
            StopAllCoroutines();
            StartCoroutine(PlayRoutine());

            IEnumerator PlayRoutine()
            {
                Color startColor = sRend.color;
                Color endColor = new Color(0,0,0,0.99f);
                for (float i = 0; i < activeTime; i += Time.deltaTime)
                {
                    sRend.color = Color.Lerp(startColor, endColor, i / activeTime);
                    yield return null;
                }
            }
        }

        public void OnSceneDestroyed()
        {
            Stop();
        }

        public void Stop()
        {
            StopAllCoroutines();
            StartCoroutine(PlayBackRoutine());

            IEnumerator PlayBackRoutine()
            {
                Color startColor = sRend.color;
                Color endColor = Color.clear;
                for (float i = 0; i < activeTime; i += Time.deltaTime)
                {
                    sRend.color = Color.Lerp(startColor, endColor, i/ activeTime);
                    yield return null;
                }
            }
        }
    }

}
