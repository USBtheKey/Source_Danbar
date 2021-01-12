
using UnityEngine;

namespace GameSystem
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] players;
        [SerializeField] private int playerShipToLoad = 0;
        // Start is called before the first frame update
        void Start()
        {
            if (!LevelManager.Instance && !GameManager.Instance) return;

            GameObjectPooler.Spawn(players[playerShipToLoad].name, this.transform.position, this.transform.rotation).SetActive(true);
        }
    }
}
