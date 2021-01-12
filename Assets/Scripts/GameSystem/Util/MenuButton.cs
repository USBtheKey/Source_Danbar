
using UnityEngine;
using UnityEngine.UI;

namespace GameSystem
{
    public class MenuButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TMPro.TextMeshProUGUI label;

        public Button Button { get => button; }
        public TMPro.TextMeshProUGUI Label { get => label; }

        private void OnEnable()
        {
            button.interactable = true;
        }

        private void OnDisable()
        {
            button.interactable = false;
        }
    }
}
