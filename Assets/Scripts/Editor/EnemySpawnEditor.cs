using Logic;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(EnemySpawner))]
    public class EnemySpawnEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(EnemySpawner spawner, GizmoType gizmo)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(spawner.transform.position, 0.3f);
        }
    }
}