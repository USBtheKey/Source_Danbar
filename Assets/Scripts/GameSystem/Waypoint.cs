
using System;
using UnityEngine;

namespace GameSystem
{
    [Serializable]
    public struct Waypoint
    {
        public bool fire;
        public float waitTime;
        public Vector3 position;
    }
}