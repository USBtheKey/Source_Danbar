
using UnityEngine;

namespace GameSystem{
    /// <summary>
    /// DO NOT DELETE - State Machine for Main Menu
    /// </summary>
    public class MainMenu : MonoBehaviour
    {
        public static MainMenu Instance { get; private set; }
        private Animator animator = null;

        private void Awake()
        {
            if (!Instance) Instance = this;
            else Destroy(this.gameObject);

            animator = GetComponent<Animator>();
        }
        private void Start()
        {
            MusicManager.Instance.PlayClip(0); // Main menu clip
        }
        private void OnDestroy()
        {
            Instance = null;
        }

        public void ChangeState(string newState)
        {
            animator.SetTrigger(newState);
        }
    }
}
