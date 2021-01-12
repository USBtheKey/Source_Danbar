
using GameSystem.Utility;
using UnityEngine;
using TMPro;

namespace GameSystem
{
    public class GameSettings
    {
        private static GameSettings instance = null;

        private Font orbitron_Font_Regular = null;
        private Font orbitron_Font_Bold = null;
        private Font orbitron_Font_Black = null;
        private Font orbitron_Font_Medium = null;


        public TMP_FontAsset orbitron_FontAsset_Regular;

        private float musicVolume = 1f;
        private float sfxVolume = 1f;
        private float masterVolume = 1f;

        private GameSettings() 
        {
            LoadFonts();

            SanityCheck();

            Debug.Log("Game Settings has been loaded.");
        }

        #region Propreties
        public float MusicVolume 
        { 
            get => musicVolume; 
            set => musicVolume = value < 0f? 0f : value > 1f? 1f: value; 
        }

        public float SfxVolume 
        { 
            get => sfxVolume; 
            set => sfxVolume = value < 0f ? 0f : value > 1f ? 1f : value;
        }

        public float MasterVolume 
        { 
            get => masterVolume; 
            set => masterVolume = value < 0f ? 0f : value > 1f ? 1f : value;
        }
        public Font Orbitron_Font_Regular 
        { 
            get => orbitron_Font_Regular; 
        }
        public Font Orbitron_Font_Bold 
        { 
            get => orbitron_Font_Bold;  
        }
        public Font Orbitron_Font_Black 
        { 
            get => orbitron_Font_Black;  
        }
        public Font Orbitron_Font_Medium 
        { 
            get => orbitron_Font_Medium; 
        }

        public static GameSettings GetInstance()
        {
            if (instance == null) instance = new GameSettings();
            return instance;
        }
        #endregion


        private void LoadFonts()
        {
            this.orbitron_Font_Regular = Resources.Load("Fonts\\Orbitron-Regular") as Font;
            this.orbitron_Font_Bold = Resources.Load("Fonts\\Orbitron-Bold") as Font;
            this.orbitron_Font_Black = Resources.Load("Fonts\\Orbitron-Black") as Font;
            this.orbitron_Font_Medium = Resources.Load("Fonts\\Orbitron-Medium") as Font;
            this.orbitron_FontAsset_Regular = Resources.Load("Fonts/Orbitron-Regular_SDF") as TMP_FontAsset;
        }

        private void SanityCheck()
        {
            if (this.orbitron_Font_Regular == null) Debug.LogError(nameof(GameSettings) + " missing resouce asset : " + nameof(orbitron_Font_Regular));
            if (this.orbitron_Font_Bold == null) Debug.LogError(nameof(GameSettings) + " missing resouce asset: " + nameof(Orbitron_Font_Bold));
            if (this.orbitron_Font_Black == null) Debug.LogError(nameof(GameSettings) + " missing resouce asset: " + nameof(Orbitron_Font_Black));
            if (this.orbitron_Font_Medium == null) Debug.LogError(nameof(GameSettings) + " missing resouce asset: " + nameof(Orbitron_Font_Medium));
            if (!this.orbitron_FontAsset_Regular) Debug.LogError(nameof(GameSettings) + " missing resouce asset: " + nameof(orbitron_FontAsset_Regular));
        }
    }
}

