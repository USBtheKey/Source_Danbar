using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using GameSystem.Statistics;

namespace GameSystem
{
    public static class SaveSystem
    {
        private static readonly string path = @"Save.Yu";
        private static readonly BinaryFormatter formatter = new BinaryFormatter();

        public static void SavePlayerData(PlayerData data)
        {
            PlayerData dataToSave = LoadPlayerData();
            dataToSave.Add(data);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                formatter.Serialize(stream, dataToSave);
            }
        }

        public static PlayerData LoadPlayerData()
        {
            PlayerData playerData = null;

            if (File.Exists(path))
            {
                using (FileStream stream = new FileStream(path, FileMode.Open))
                {
                    if(stream.Length > 0 ) playerData = formatter.Deserialize(stream) as PlayerData;
                    else playerData = new PlayerData();
                }
            }
            else
            {
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    playerData = new PlayerData();
                }
                Debug.LogError("File Not Found: " + path);
            }
            Debug.Log(playerData.ToString());
            return playerData;
        }

        public static void HappyFace()
        {
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                formatter.Serialize(stream, new PlayerData());
            }
        }
    }
}

