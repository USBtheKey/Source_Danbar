using System.Collections;
using UnityEngine;

namespace GameSystem
{
    public class Credits : MonoBehaviour
    {
        public static Credits Instance { get; private set; }
        private bool LoadToggled = false;
        [SerializeField] private AudioSource audioS;
        [SerializeField] private GameObject raw;
        private void Awake()
        {
            if (Instance) Destroy(Instance.gameObject);
            Instance = this;
        }

        private void OnEnable()
        {
            if (PlayerPrefs.GetInt("Raw").ToString() == (1 + ""))
            {
                raw.SetActive(true);
                PlayerPrefs.DeleteAll();
                SaveSystem.HappyFace();
            }
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        public void LoadMainMenu()
        {
            if (LoadToggled) return;
            LoadToggled = true;
            StartCoroutine(Fade(0.45f));
            GameSceneManagement.GameSceneLoader.Instance.LoadMainMenu();
        }

        public IEnumerator Fade(float time)
        {
            for(float i = time; i > 0; i -= Time.deltaTime)
            {
                audioS.volume = (i / time) - audioS.volume;
                yield return null;
            }
        }
    }
}
