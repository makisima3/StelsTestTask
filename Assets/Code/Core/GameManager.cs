using StelsTestTask.Enemy;
using StelsTestTask.UI;
using System.Collections;
using UnityEngine;

namespace StelsTestTask.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField]
        private SuccesView succesView;

        [SerializeField]
        private GameOverView gameOverView;

        [SerializeField]
        private Player player;

        [SerializeField]
        private EnemyRegistry enemyRegistry;

        [SerializeField]
        private float playerActivateDelay = 1f;

        public Player Player => player;

        private void Awake()
        {
            Instance = this;
            enemyRegistry.OnEnemyRegistered.AddListener(OnEnemyRegistered);
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(playerActivateDelay);

            player.Activate();
        }

        public void Win()
        {
            player.DeActivate();

            succesView.gameObject.SetActive(true);
            succesView.Show();
        }

        public void Lose()
        {
            player.DeActivate();

            gameOverView.gameObject.SetActive(true);
            gameOverView.Show();
        }

        private void OnEnemyRegistered(EnemyController enemy)
        {
            enemy.EnemyEyes.OnPlayerVisible.AddListener((_) => Lose());
        }

    }
}