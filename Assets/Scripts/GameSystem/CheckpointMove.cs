
using GameSystem;
using System;
using System.Collections;
using UnityEngine;

public class CheckpointMove: MonoBehaviour
{
    public float MoveSpeed;
    private Waypoint[] waypoints;
    private int currWaypointIndex = 0;

    private void OnEnable()
    {
        if (waypoints == null) return;
        Move();
    }

    private void Update()
    {
        if (waypoints == null) return;

        Debug.DrawLine(transform.position, waypoints[currWaypointIndex].position, Color.red);
    }

    public void Move()
    {
        PlaceSelfAtStartingLocation();
        StartCoroutine(Move_Routine());

        IEnumerator Move_Routine()
        {
            while(true)
            {

                Vector3 roughDirection = waypoints[currWaypointIndex].position - transform.position;

                if (roughDirection.magnitude <= 10f) //Arrived at destination.
                {

                    if (waypoints[currWaypointIndex].waitTime > 0)
                    {
                        yield return new WaitForSeconds(waypoints[currWaypointIndex].waitTime);
                    }

                    if (waypoints[currWaypointIndex].fire)
                    {
                        OnFiringPositionReached?.Invoke();
                    }

                    if(currWaypointIndex + 1 < waypoints.Length) //Not reached last checkpoint
                    {
                        currWaypointIndex++;
                        OnWaypointChange?.Invoke(waypoints[currWaypointIndex].position);
                    }
                    else // Reached last checkpoint
                    {
                        break;
                    }
                }

                transform.Translate(roughDirection.normalized * MoveSpeed * Time.deltaTime, Space.World);

                yield return null;
            }

            OnFinalDestinationReached?.Invoke();
        }
    }

    private void OnDisable()
    {
        waypoints = null;
    }

    public void SetCheckpoints(Waypoint[] waypoints)
    {
        this.waypoints = waypoints;
    }

    private void PlaceSelfAtStartingLocation()
    {
        currWaypointIndex = 0;
        gameObject.transform.position = waypoints[0].position;
    }

    public event Action OnFiringPositionReached;
    public event Action<Vector3> OnWaypointChange;
    public event Action OnFinalDestinationReached;
}
