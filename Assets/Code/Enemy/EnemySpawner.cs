using StelsTestTask.Core;
using StelsTestTask.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace StelsTestTask.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private EnemyController enemyPrefab;

        [SerializeField]
        private List<Waypoint> spawnPoints;

        [SerializeField]
        private EnemyRegistry enemyRegistry;

        // Start is called before the first frame update
        void Start()
        {
            foreach (var point in spawnPoints)
            {
                var enemy = Spawn(point.Point.position, point);
                enemyRegistry.Register(enemy);
            }
        }

        public EnemyController Spawn(Vector3 position, Waypoint waypoint)
        {
            var enemy = Instantiate(enemyPrefab.gameObject).GetComponent<EnemyController>();

            position.y = 0.255f;
            enemy.transform.position = position;
            enemy.WaypointAgent.SetFirstWaypoint(waypoint);
            enemy.EnemyEyes.Target = GameManager.Instance.Player.transform;

            return enemy;
        }
    }
}