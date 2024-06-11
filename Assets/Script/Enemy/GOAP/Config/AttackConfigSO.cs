using UnityEngine;
using UnityEngine.Rendering;

namespace Enemy.GOAP.Config
{
    [CreateAssetMenu(menuName = "AI/AttackConfig", fileName = "Attack Config", order = 1)]

    public class AttackConfigSO : ScriptableObject
    {
        public float sensorRadius = 10;
        public float MeleeAttackRadius = 1f;
        public float AttackDelay = 1f;
        public int MeleeAttackCost = 1;
        public LayerMask AttackableLayerMask;
    }

}