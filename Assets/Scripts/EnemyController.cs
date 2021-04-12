using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts
{
    [RequireComponent(typeof(WaypointAgent))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private float rotateSpeed;
        [SerializeField]
        private float movementSpeed = 5f;

        private WaypointAgent waypointAgent;

        private Tween mainTween;

        private void Awake()
        {
            waypointAgent = GetComponent<WaypointAgent>();

            waypointAgent.OnPositionReached.AddListener(OnPosotionReached);
        }

        private void OnPosotionReached(Waypoint waypoint)
        {
            RotateTo(waypoint.Point.position);
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