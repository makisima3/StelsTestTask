using System;
using UnityEngine;
using UnityEngine.Events;

namespace StelsTestTask.Events
{
    [Serializable]
    public class OnPlayerVisible : UnityEvent<GameObject> { } //событие Unity для обработки вхождения игрока в область видимости
}