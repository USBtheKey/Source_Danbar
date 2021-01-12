

using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameSystem.Utility
{
    public class FontDictator : MonoBehaviour
    {

        private void Start()
        {
            GameSettings settingInstance = GameSettings.GetInstance();

            Text[] txts = Resources.FindObjectsOfTypeAll<Text>();
#if UNITY_EDITOR
            Debug.Log("Legacy GUI count: " + txts.Length);
#endif
            foreach (Text txt in txts)
            {
                txt.font = settingInstance.Orbitron_Font_Regular;
#if UNITY_EDITOR
                Debug.LogWarning("Legacy Text Component", txt);
#endif
            }

            TextMeshProUGUI[] newTxts = Resources.FindObjectsOfTypeAll<TextMeshProUGUI>();
            foreach(TextMeshProUGUI txt in newTxts)
            {
                txt.font = settingInstance.orbitron_FontAsset_Regular;
            }
        }
    }
}
