using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem
{
    public class Path : MonoBehaviour
    {

        [SerializeField] private int mirrorNode = 0;
        
        private Waypoint[] waypoints;
        private Transform[] checkpointTrans;

        private void Awake()
        {
            FindCheckpoints();
        }

        private void OnDrawGizmos()
        {
            if (Application.isPlaying) return;

            waypoints = null;
            FindCheckpoints();

            Gizmos.DrawSphere(transform.position, 10);

            if (waypoints == null)
                return;

            for (int i = 0; i < waypoints.Length - 1; i++)
            {
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
            }

            for (int i = 0; i < waypoints.Length; i++)
            {
                Gizmos.DrawIcon(waypoints[i].position + new Vector3(20, 20, 0), "Numbering/" + i.ToString(), true, Color.red);
                Gizmos.DrawSphere(waypoints[i].position, 10);
            }
        }

        public Waypoint[] GetWaypoints()
        {
            if(waypoints == null)
            {
                FindCheckpoints();
            }
            return waypoints;
        }

        [ContextMenu("Flip Path Vectically")]
        private void FlipPathVertically()
        {
            Transform[] trans = GetComponentsInChildren<Transform>(true);
            checkpointTrans = new Transform[trans.Length - 1];

            if (mirrorNode > checkpointTrans.Length - 1)
            {
                Debug.LogError("Index out of bound error. Max number: " + (checkpointTrans.Length - 1));
                
            }
            else
            {
                Array.Copy(trans, 1, checkpointTrans, 0, trans.Length - 1);

                for (int i = 0; i < checkpointTrans.Length; i++)
                {
                    float distance = Math.Abs(checkpointTrans[i].position.x - checkpointTrans[mirrorNode].position.x);

                    if (checkpointTrans[i].position.x > checkpointTrans[mirrorNode].position.x)
                    {
                        checkpointTrans[i].position = new Vector3(checkpointTrans[mirrorNode].position.x - distance, checkpointTrans[i].position.y);
                    }
                    else
                    {
                        checkpointTrans[i].position = new Vector3(checkpointTrans[mirrorNode].position.x + distance, checkpointTrans[i].position.y);
                    }
                }
            }
        }

        [ContextMenu("Flip Path Horizontally")]
        private void FlipPathHorizontally()
        {
            Transform[] trans = GetComponentsInChildren<Transform>(true);
            checkpointTrans = new Transform[trans.Length - 1];

            if (mirrorNode > checkpointTrans.Length - 1)
            {
                Debug.LogError("Index out of bound error. Max number: " + (checkpointTrans.Length - 1));
            }
            else
            {

                Array.Copy(trans, 1, checkpointTrans, 0, trans.Length - 1);

                for (int i = 0; i < checkpointTrans.Length; i++)
                {
                    float distance = Math.Abs(checkpointTrans[i].position.y - checkpointTrans[mirrorNode].position.y);

                    if (checkpointTrans[i].position.y > checkpointTrans[mirrorNode].position.y)
                    {
                        checkpointTrans[i].position = new Vector3(checkpointTrans[i].position.x, checkpointTrans[mirrorNode].position.y - distance);
                    }
                    else
                    {
                        checkpointTrans[i].position = new Vector3(checkpointTrans[i].position.x, checkpointTrans[mirrorNode].position.y + distance);
                    }
                }
            }
        }

        //[ContextMenu("Mirror Horizontally")]
        //private void MirrorHorizontally()
        //{
        //    Transform[] trans = GetComponentsInChildren<Transform>(true);
        //    checkpointTrans = trans;

        //    foreach (Transform t in checkpointTrans)
        //    {
        //        t.position = new Vector3(t.position.x, -t.position.y, t.position.z);
        //    }
        //}

        private void FindCheckpoints()
        {
            Checkpoint[] monoWaypoints = GetComponentsInChildren<Checkpoint>(true);
            List<Waypoint> waypoints = new List<Waypoint>();
            
            foreach(Checkpoint monoWaypoint in monoWaypoints)
            {
                waypoints.Add(monoWaypoint.ToWaypoint());
            }
            this.waypoints = waypoints.ToArray();
        }
    }
}
