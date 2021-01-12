
using System;
using UnityEngine;
using TMPro;

namespace GameSystem.UI
{

    public class PlayerHUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI ammoText;
        [SerializeField] private TextMeshProUGUI lifeText;
        [SerializeField] private TextMeshProUGUI missileText;

        public void UpdateAmmoText(int num) => ammoText.text = Format(num, 3);
        public void UpdateLifeText(int num) => lifeText.text = num.ToString();
        public void UpdateMissileText(int num) => missileText.text = Format(num, 3);


        public void Disable()
        {
            foreach (Transform t in transform) t.gameObject.SetActive(false);
        }

        public void Display()
        {
            foreach (Transform t in transform) t.gameObject.SetActive(true);
        }

        private string Format(int num, int maxDigits)
        {
            var digits = 0;
            var str = "";
            for (int i = num; i > 0; i /= 10) digits += 1;

            if (digits > maxDigits) return "ERR";
            else
            {
                var placeholders = maxDigits - digits; 
                for (int i = 0; i < placeholders; i++) str += "0";
                if (digits > 0) str += $"{num}";
            }
            return str;
        }
    }
}
