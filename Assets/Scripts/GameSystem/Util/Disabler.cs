using UnityEngine;

namespace GameSystem
{
    public class Disabler : MonoBehaviour {
        private void OnEnable()
        {
            if (!LevelManager.Instance) gameObject.SetActive(false);
        }
    }
}



