using Assets.Scripts.Utils;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts
{
    [RequireComponent(typeof(WaypointAgent))]
    [RequireComponent(typeof(EnemyEyes))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private float rotateSpeed = 150f;
        [SerializeField]
        private float movementSpeed = 1f;

        private WaypointAgent waypointAgent;
        private EnemyEyes enemyEyes;
        private Tween mainTween;

        private void Awake()
        {
            waypointAgent = GetComponent<WaypointAgent>();
            enemyEyes = GetComponent<EnemyEyes>();

            waypointAgent.OnPositionReached.AddListener(OnPosotionReached);
            enemyEyes.OnPlayerVisible.AddListener(OnPlayerVisible);
        }

        private void OnPosotionReached(Waypoint waypoint)
        {
            RotateTo(waypoint.Point.position);
        }

        private void OnPlayerVisible(GameObject detectBy)
        {
            Debug.Log("Опа, ливер вылез!");
        }

        private void RotateTo(Vector3 target)
        {
            var direction = (target - transform.position).normalized;
            direction.y = 0.0f;

            var angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg + 90f;

            if(mainTween != null)
            {
                mainTween.Kill();
            }

            mainTween = transform.DORotate(Vector3.up * angle, Mathf.Abs(angle - transform.eulerAngles.y) / rotateSpeed).SetEase(Ease.OutSine).OnComplete(() => MoveTo(target));
        }

        private void MoveTo(Vector3 target)
        {
            target.y = transform.position.y;

            if (mainTween != null)
            {
                mainTween.Kill();
            }

            mainTween = transform.DOMove(target, target.magnitude / movementSpeed ).SetEase(Ease.InOutSine);
        }
    }
}