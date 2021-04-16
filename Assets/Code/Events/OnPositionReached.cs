using StelsTestTask.Utils;
using System;
using UnityEngine.Events;

namespace StelsTestTask.Events
{
    [Serializable]
    public class OnPositionReached : UnityEvent<Waypoint> { } //событие Unity для обработки достижения цели (Waypoint) агентом (WaypointAgent)
}
