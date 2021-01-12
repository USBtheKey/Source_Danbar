using GameSystem;
using GameSystem.Statistics;

using UnityEngine;
using UnityEngine.UI;

public class PanelLevelSelect : MonoBehaviour
{

    private PlayerData data;
    private int yuCounter = 0;
    [SerializeField] private Button l2;
    [SerializeField] private Button l3;
    [SerializeField] private GameObject maybe;

    private void OnEnable()
    {
        data = SaveSystem.LoadPlayerData();
        l2.gameObject.SetActive(data.Level2Unlocked);
        l3.gameObject.SetActive(data.Level3Unlocked);
        maybe.SetActive(data.Level4Unlocked);
    }

    public void YuCounter()
    {
        if (++yuCounter >= 40)
        {
            PlayerPrefs.SetInt("Raw", 1);
            PlayerPrefs.Save();
        }
    }
}
