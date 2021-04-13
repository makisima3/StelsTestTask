using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Utils
{
    [Serializable]
    public class OnPlayerVisible : UnityEvent<GameObject> { }
}