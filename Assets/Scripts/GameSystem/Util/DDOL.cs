
using UnityEngine;

namespace GameSystem.Utility
{
    public class DDOL : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
