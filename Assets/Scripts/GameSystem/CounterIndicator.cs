
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GameSystem
{
    [RequireComponent(typeof(Image), typeof(CanvasRenderer))]
    public class CounterIndicator : MonoBehaviour
    {
        protected Image image;
        protected Coroutine CDCoroutine;
        protected bool paused = false;
        protected virtual void Awake()
        {
            image = GetComponent<Image>();
        }

        protected void OnEnable()
        {
            ClearReset();
        }

        public void StartCounting(float cooldownTime, Action OnCooldownOver)
        {
            if (CDCoroutine != null) return;

            CDCoroutine = StartCoroutine(CDRoutine());

            IEnumerator CDRoutine()
            {
                ClearReset();
                var start = image.fillAmount;
                var timeElapsed = 0f;

                while (timeElapsed <= cooldownTime)
                {
                    if (!paused)
                    {
                        image.fillAmount = Mathf.Lerp(start, 1, Mathf.Clamp01(timeElapsed / cooldownTime));
                        timeElapsed += Time.deltaTime;
                    }
                    yield return null;
                }
                OnCooldownOver.Invoke();
                ClearReset();
            }
        }

        public void ClearReset()
        {
            CDCoroutine = null;

            image.fillAmount = 0f;
            image.type = Image.Type.Filled;
            image.fillAmount = 0;
        }

        public void Stop() {
            StopAllCoroutines();
            CDCoroutine = null;
        }
        public void Pause() => paused = true;
        public void UnPause() => paused = false;
    }
}
