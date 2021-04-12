using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class WaypointAgent : MonoBehaviour
    {
        [SerializeField]
        private Waypoint firstWaypoint;

        [SerializeField]
        private OnPositionReached onPositionReached;

        private Waypoint curentWaypoint;

        public OnPositionReached OnPositionReached => onPositionReached;

        private void Awake()
        {
            curentWaypoint = firstWaypoint;
        }

        private void Start()
        {
            onPositionReached.Invoke(curentWaypoint);
        }

        private void FixedUpdate()
        {
            var curentPosition = transform.position;
            curentPosition.y = 0f;

            var targetPosition = curentWaypoint.Point.position;
            targetPosition.y = 0f;

            if(Vector3.Distance(curentPosition,targetPosition) <= Mathf.Epsilon)
            {
                curentWaypoint = curentWaypoint.Next;

                onPositionReached.Invoke(curentWaypoint);
            }

        }
    }
}