using TMPro;
using UnityEngine;

public class ApplicationVersion : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<TextMeshProUGUI>().text = $"Version: {Application.version}";
    }
}
