using GameSystem.UI;
using GameSystem.Weapons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem
{
    public class GunsSystem : MonoBehaviour
    {
        [SerializeField] private int ammo = 0;
        [SerializeField] private int maxAmmo = 333;
        [HideInInspector] public Weapon[] guns { private set; get; }
        [SerializeField] private PlayerHUD hud;
        [SerializeField] private CounterIndicator ammoReload;
        private bool isReloading = false;
        public float ReloadTime = 1f;

        public int Ammo
        {
            get => ammo;
            set 
            {
                ammo = value < 0 ? 0 : value > maxAmmo ? maxAmmo : value;
                OnAmmoUpdate?.Invoke(ammo);
                if (ammo <= 0) Reload();
            }
        }

        private void Awake()
        {
            guns = GetComponentsInChildren<Weapon>(true);
        }

        private void OnEnable()
        {
            if (!LevelManager.Instance) return;

            OnAmmoUpdate += hud.UpdateAmmoText;

            foreach (Weapon weap in guns)
            {
                weap.OnFire += ConsumeAmmo;
                weap.OnFire += LevelManager.Instance.PlayerLevelData.IncrementBullets;
            }

            Ammo = maxAmmo;
        }

        private void OnDisable()
        {
            if (!LevelManager.Instance) return;

            OnAmmoUpdate -= hud.UpdateAmmoText;

            foreach (Weapon weap in guns)
            {
                weap.OnFire -= ConsumeAmmo;
                weap.OnFire -= LevelManager.Instance.PlayerLevelData.IncrementBullets;
            }
        }

        public void Fire()
        {
            if (ammo - guns.Length >= 0) Array.ForEach(guns, gun => gun.Fire());
        }

        public void HoldFire() => Array.ForEach(guns, gun => gun.HoldFire());

        private void ConsumeAmmo()
        {
            Ammo--;
        }

        private void Reload()
        {
            if (isReloading) return;

            isReloading = true;
            HoldFire();
            ammoReload.StartCounting(ReloadTime, () =>
            {
                ReplenishAmmunitions();
                isReloading = false;
            });
        }

        public void CancelReload()
        {
            ammoReload.Stop();
            isReloading = false;
        }

        public void ReplenishAmmunitions() => Ammo = maxAmmo;

        public event Action<int> OnAmmoUpdate;
    }
}
