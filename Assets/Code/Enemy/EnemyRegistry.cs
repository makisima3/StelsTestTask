using StelsTestTask.Events;
using System.Collections.Generic;
using UnityEngine;

namespace StelsTestTask.Enemy
{
    public class EnemyRegistry : MonoBehaviour
    {
        [SerializeField]
        private List<EnemyController> registeredEnemies;

        [SerializeField]
        private OnEnemyRegistered onEnemyRegistered;

        public OnEnemyRegistered OnEnemyRegistered => onEnemyRegistered;

        public void Register(EnemyController enemy)
        {
            registeredEnemies.Add(enemy);
            onEnemyRegistered.Invoke(enemy);
        }

    }
}