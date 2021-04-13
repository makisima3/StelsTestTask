using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts
{
	public class EnemyEyes : MonoBehaviour
	{
		[SerializeField]
		private float viewRadius;
		[SerializeField, Range(0, 360)]
		private float viewAngle;

		[SerializeField]
		private LayerMask obstacleMask;
		[SerializeField]
		private Transform target;

		[SerializeField]
		private OnPlayerVisible onPlayerVisible;

		public float ViewRadius => viewRadius;
		public float ViewAngle => viewAngle;
		public OnPlayerVisible OnPlayerVisible => onPlayerVisible;
		public Transform Target => target;

        void FixedUpdate()
		{
			FindTarget();
		}

		float rightBorder = 0f;
		float leftBorder = 0f;
		float testAngle = 0f;
		private void FindTarget()
		{
			var vectorToTarget = target.position - transform.position;
			var direction = vectorToTarget.normalized;
			var distance = vectorToTarget.magnitude;

			rightBorder = transform.eulerAngles.y + (viewAngle / 2f);
			leftBorder = transform.eulerAngles.y - (viewAngle / 2f);

			if (distance < viewRadius)
			{
				testAngle = DirectionToAngle(direction);
				
				if (testAngle > leftBorder && testAngle < rightBorder)
				{
					onPlayerVisible.Invoke(gameObject);
				}
			}
		}
		
		private float DirectionToAngle(Vector3 direction)
		{
			// 90 чтобы ось z (forward vector) соответствовала повороту начитнающемуся от оси x
			
			return 90f - Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg; 
		}

		public Vector3 AngleToDirection(float angleInDegrees)
		{
			//хз как проверить вхождение одного направления в другое. толь ко через углы погуглю
			return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
		}

		private void OnDrawGizmos()
        {
			Gizmos.color = Color.white;
			Gizmos.DrawWireSphere(transform.position, viewRadius);
			Gizmos.color = Color.red;
			Gizmos.DrawLine(transform.position, transform.position + AngleToDirection(testAngle) * viewRadius);
			Gizmos.color = Color.blue / 2f;
			Gizmos.DrawLine(transform.position, transform.position + AngleToDirection(leftBorder) * viewRadius);
			Gizmos.DrawLine(transform.position, transform.position + AngleToDirection(rightBorder) * viewRadius);
		}
	}
}