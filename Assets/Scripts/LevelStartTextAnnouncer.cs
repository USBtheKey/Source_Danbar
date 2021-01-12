
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystem
{

    public class LevelStartTextAnnouncer : MonoBehaviour
    {
        //Called by Animation Event
        public void GrabLevelName()    //Yes it is freaking ghetto lmao.
        {
            var name = SceneManager.GetActiveScene().name;
            string tmp = "Placeholder";

            if (name == "Level_1_Scene") tmp = "Stage 1";
            else if (name == "Level_2_Scene") tmp = "Stage 2";
            else if(name == "Level_3_Scene") tmp = "Stage 3";
            GetComponent<TextMeshProUGUI>().text = tmp;
        }
    }
}

