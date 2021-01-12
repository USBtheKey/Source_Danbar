
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace GameSystem
{
    public class SetVolume : MonoBehaviour
    {
        public enum AudioChannel { volMaster, volMusic, volSFX }
        private AudioMixer am;
        private Slider slider;

        [SerializeField] private AudioChannel channelName = AudioChannel.volMaster;
        private const float defaultVol = 0.5f;


        private void Start()
        {
            slider = GetComponent<Slider>();

            if (MusicManager.Instance) am = MusicManager.Instance.AudioM;
            else Debug.Log(" Music Manager is missing.");


            slider.value = PlayerPrefs.GetFloat(channelName.ToString(), defaultVol);
        }

        public void Set(float val)
        {
            float num = val <= 0 ? -80f : Mathf.Log10(val) * 20;

            if (am)
            {
                am.SetFloat(channelName.ToString(), num);
            }
                

            PlayerPrefs.SetFloat(channelName.ToString(), slider.value);
        }

    }
}
