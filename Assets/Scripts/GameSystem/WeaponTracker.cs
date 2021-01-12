

using UnityEngine;

namespace GameSystem
{
    public class WeaponTracker : MonoBehaviour
    {
        private enum WeaponTrackerTag { Player, Enemy };
        [SerializeField] private bool CHEAT_TRACK = false;
        private static Transform player;
        [SerializeField] private WeaponTrackerTag targetTag = WeaponTrackerTag.Player;
        private Transform target;
        private void OnEnable()
        {

            if(!CHEAT_TRACK) if (!LevelManager.Instance) return;

            var obj = GameObject.FindGameObjectWithTag(targetTag.ToString());
            if (targetTag == WeaponTrackerTag.Player)
            {
                if(obj) player = obj.transform;
                else Debug.LogWarning(targetTag + " not found.", this);
            }
            else
            {
                if (obj) target = obj.transform;
                else Debug.LogWarning(targetTag + " not found.", this);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!target && !player) return;

            if (targetTag == WeaponTrackerTag.Player) transform.up = player.position - transform.position;
            else transform.up = target.position - transform.position;

        }
    }
}

