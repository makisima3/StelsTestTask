using UnityEngine;
using UnityEditor;

namespace Assets.Scripts.EditorNS
{
	[CustomEditor(typeof(EnemyEyes))]
	public class EnemyEyesEditor : Editor
	{
		private EnemyEyes Eyes => (EnemyEyes)target;

        void _OnSceneGUI()
		{
			
			Handles.color = Color.white;
			Handles.DrawWireArc(Eyes.transform.position, Vector3.up, Vector3.forward, 360, Eyes.ViewRadius);
			Vector3 viewAngleA = Eyes.AngleToDirection(Eyes.transform.eulerAngles.y - Eyes.ViewAngle / 2f);
			Vector3 viewAngleB = Eyes.AngleToDirection(Eyes.transform.eulerAngles.y + Eyes.ViewAngle / 2f);

			Handles.DrawLine(Eyes.transform.position, Eyes.transform.position + viewAngleA * Eyes.ViewRadius);
			Handles.DrawLine(Eyes.transform.position, Eyes.transform.position + viewAngleB * Eyes.ViewRadius);

			Handles.color = Color.red;
			if (Eyes.Target)
			{
				Handles.DrawLine(Eyes.transform.position, Eyes.Target.position);
			}
		}
	}
}