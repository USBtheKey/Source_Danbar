using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_BossHealthBar : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI lifeLeftTxt;
    [SerializeField] private Image healthBarImg;

    public void UpdateHealthRatio(float healthPercent)
    {
        healthBarImg.fillAmount = healthPercent;
    }

    public void UpdateLife(int life)
    {
        lifeLeftTxt.text = "x " + (life + 1);
    }

    public void DisableChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void EnableChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
