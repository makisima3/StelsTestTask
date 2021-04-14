using System;
using UnityEngine.Events;

namespace Assets.Scripts.Utils
{
    [Serializable]
    public class OnPositionReached : UnityEvent<Waypoint> { } //событие Unity для обработки достижения цели (Waypoint) агентом (WaypointAgent)
}
