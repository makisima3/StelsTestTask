using StelsTestTask.Enemy;
using System;
using UnityEngine.Events;

namespace StelsTestTask.Events
{
    [Serializable]
    public class OnEnemyRegistered : UnityEvent<EnemyController>
    {

    }
}
