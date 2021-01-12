
using UnityEngine;

namespace GameSystem
{
    public class OtherGameAssetsInstance : MonoBehaviour
    {
        private static OtherGameAssetsInstance instance;

        private void Awake()
        {
            CreateInstance();

            void CreateInstance()
            {
                if (!instance)
                {
                    instance = this;
                    DontDestroyOnLoad(this.gameObject);
                }
                else Destroy(this.gameObject);
            }
        }
    }

}
