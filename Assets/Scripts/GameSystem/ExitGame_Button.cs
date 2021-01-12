using GameSystem;
using UnityEngine;

public class ExitGame_Button : MonoBehaviour
{
    public void ExitGame()
    {
        GameManager.Instance.ExitApplication();
    }
}
