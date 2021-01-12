using System.Collections;
using UnityEngine;

namespace GameSystem
{

    public class Checkpoint : MonoBehaviour
    {
        [SerializeField] private bool fire = false;
        [SerializeField] private float waitTime = 0f;

        public bool Fire 
        { 
            get => fire;  
        }

        public float WaitTime 
        { 
            get => waitTime;
        }

        public Vector3 Position
        {
            get => this.transform.position;
        }

        public Waypoint ToWaypoint()
        {
            return new Waypoint()
            {
                fire = this.fire,
                waitTime = this.waitTime,
                position = this.transform.position
            };
        }
    }
}