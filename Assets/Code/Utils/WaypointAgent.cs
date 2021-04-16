using StelsTestTask.Events;
using UnityEngine;

namespace StelsTestTask.Utils
{
    //Агент движения по точкам пути
    public class WaypointAgent : MonoBehaviour
    {
        [SerializeField]
        private Waypoint firstWaypoint;

        [SerializeField]
        private OnPositionReached onPositionReached;


        //точка в которую в данный момент движется агент
        private Waypoint curentWaypoint;

        public OnPositionReached OnPositionReached => onPositionReached;

        private void Awake()
        {
        }

        private void Start()
        {
            curentWaypoint = firstWaypoint;
            //начальное срабатывание для начала движения к первой точке (если позиция отличалась от целевой)
            onPositionReached.Invoke(curentWaypoint);
        }

        private void Update()
        {
            var curentPosition = transform.position;
            curentPosition.y = 0f; // игнорирование высоты

            var targetPosition = curentWaypoint.Point.position;
            targetPosition.y = 0f; // игнорирование высоты

            //если расстояние до целевой точки достаточно мало
            if (Vector3.Distance(curentPosition,targetPosition) <= 1e-5)
            {
                //переключение точки
                curentWaypoint = curentWaypoint.Next;
                //вызов события
                onPositionReached.Invoke(curentWaypoint);
            }

        }

        public void SetFirstWaypoint(Waypoint waypoint)
        {
            firstWaypoint = waypoint;
        }
    }
}