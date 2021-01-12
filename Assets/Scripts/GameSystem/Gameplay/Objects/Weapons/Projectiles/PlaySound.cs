using UnityEngine;

namespace GameSystem
{
    [RequireComponent(typeof(AudioSource))]
    public class PlaySound: MonoBehaviour
    {
        private AudioSource audioS;

        [SerializeField] private AudioClip clip;

        private void Awake()
        {
            audioS = GetComponent<AudioSource>();

            audioS.playOnAwake = false;
            audioS.loop = true;
        }

        private void OnEnable()
        {
            if (!LevelManager.Instance) return;

            if(clip) audioS.PlayOneShot(clip);
        }
    }
}