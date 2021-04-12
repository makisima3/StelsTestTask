﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace Assets.Scripts.Utils
{
    [Serializable]
    public class OnPositionReached : UnityEvent<Waypoint>
    {
    }
}
