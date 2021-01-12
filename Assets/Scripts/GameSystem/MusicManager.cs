using System.Collections;
using System.Collections.Generic;

using GameSystem.Utility;
using UnityEngine;
using UnityEngine.Audio;

namespace GameSystem
{

    [RequireComponent(typeof(AudioSource))]
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager Instance { get; private set; }

        public AudioMixer AudioM { get; private set; }
        private AudioMixerSnapshot pausedSnapshot;
        private AudioMixerSnapshot unpausedSnapshot;
        [SerializeField] private AudioClip[] audioClips = null;

        private AudioSource audioS = null;

        private const string channelMusic = "volMusic";
        private const string channelSFX = "volSFX";
        private const string channelMaster = "volMaster";

        private const float defaultVol = 1f;

        private void Awake()
        {
            CreateInstance();

            audioS = GetComponent<AudioSource>();
            AudioM = Resources.Load("AudioMixer") as AudioMixer;

            pausedSnapshot = AudioM.FindSnapshot("Paused");
            unpausedSnapshot = AudioM.FindSnapshot("Unpaused");
        }

        private void OnEnable()
        {
            GameSceneManagement.GameSceneLoader.OnStartLoadingNextScene += DecreaseVolume;
            GameSceneManagement.GameSceneLoader.OnSceneTransitioning += OnUnpausePlay;

            audioS.loop = true;
        }

        private void Start()
        {
            AudioM.SetFloat(channelMusic, Util.FloatToVol(PlayerPrefs.GetFloat(channelMusic, defaultVol)));
            AudioM.SetFloat(channelMaster, Util.FloatToVol(PlayerPrefs.GetFloat(channelMaster, defaultVol)));
            AudioM.SetFloat(channelSFX, Util.FloatToVol(PlayerPrefs.GetFloat(channelSFX, defaultVol)));
        }

        private void OnDisable()
        {
            GameSceneManagement.GameSceneLoader.OnStartLoadingNextScene -= DecreaseVolume;
        }

        public void PlayClip(int index)
        {
            if (index < 0 || index > audioClips.Length)
            {
                index = 0;// Main menu music
                Debug.LogError(nameof(MusicManager) + " ERROR: Music clip is out of bound.");
                return;
            }
            
            audioS.clip = audioClips[index];
            audioS.Play();
        }

        public void DecreaseVolume()
        {
            var start = audioS.volume;
            var end = 0f;
            var timeElapse = 0f;
            var t = 0f;

            StartCoroutine(DecreaseVolumeRoutine());

            IEnumerator DecreaseVolumeRoutine()
            {
                while (audioS.volume > 0)
                {
                    audioS.volume = Mathf.Lerp(start, end, t);
                   // Debug.Log("Volume : " + audioS.volume);
                    timeElapse += Time.deltaTime;
                    t = timeElapse / GameSceneManagement.SceneTransition.FadeTime;
                    yield return null;
                }

                audioS.Stop();
            }
        }

        public void IncreaseVolume()
        {
            var start = audioS.volume;
            var end = 1f;
            var timeElapse = 0f;
            var t = 0f;

            StartCoroutine(IncreaseVolumeRoutine());

            IEnumerator IncreaseVolumeRoutine()
            {
                while (audioS.volume < 1f)
                {
                    audioS.volume = Mathf.Lerp(start, end, t);
                   // Debug.Log("Volume : " + audioS.volume);
                    timeElapse += Time.deltaTime;
                    t = timeElapse / GameSceneManagement.SceneTransition.FadeTime;
                    yield return null;
                }
            }
        }

        private void CreateInstance()
        {
            if (!Instance) Instance = this;
            else Destroy(this.gameObject);
        }

        public void OnPausePlay()
        {
            pausedSnapshot.TransitionTo(0f);
        }

        public void OnUnpausePlay()
        {
            unpausedSnapshot.TransitionTo(0f);
        } 
    }
}
