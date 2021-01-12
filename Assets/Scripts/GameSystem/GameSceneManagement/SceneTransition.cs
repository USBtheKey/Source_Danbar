using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GameSystem.GameSceneManagement
{
    [RequireComponent(typeof(Image))]
    public class SceneTransition : MonoBehaviour
    {
        public static SceneTransition Instance { get; private set; }
        public Animation Anim { private set; get; }
        public static float FadeTime { get; private set; } = 1f;

        private enum Transitions { SceneTransitionFadeIn, SceneTransitionFadeOut };

        private void Awake()
        {
            CreateInstance();

            Anim = GetComponent<Animation>();

            void CreateInstance()
            {
                if (!Instance) Instance = this;
                else Destroy(this.gameObject);
            }
        }

        private void OnEnable()
        {
            GameSceneLoader.OnStartLoadingNextScene += FadeOut;
        }

        private void OnDisable()
        {
            GameSceneLoader.OnStartLoadingNextScene -= FadeOut;
        }

        public void FadeOut()
        {
            Anim.Play($"{Transitions.SceneTransitionFadeOut}");
        }

        public void FadeIn()
        {
            Anim.Play($"{Transitions.SceneTransitionFadeIn}");
        }
    }
}

