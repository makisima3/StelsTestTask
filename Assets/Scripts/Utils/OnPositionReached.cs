﻿using System;
using UnityEngine.Events;

namespace Assets.Scripts.Utils
{
    [Serializable]
    public class OnPositionReached : UnityEvent<Waypoint> { }
}
