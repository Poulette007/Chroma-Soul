using System;
using CrashKonijn.Goap.Behaviours;
using Enemy.GOAP.Config;
using Enemy.GOAP.Goals;
using Enemy.Sensors;
using UnityEngine;

namespace Enemy.GOAP.Behaviors
{
    [RequireComponent(typeof(AgentBehaviour))]
    public class OrcBrain : MonoBehaviour
    {
        [SerializeField] private PlayerSensor playerSensor;
        [SerializeField] private AttackConfigSO attackConfig;
        private AgentBehaviour agentBehaviour;

        private void Awake()
        {
            agentBehaviour = GetComponent<AgentBehaviour>();
        }
        private void Start()
        {
            agentBehaviour.SetGoal<WanderGoal>(false);
            var capsuleCollider = playerSensor.Collider as CapsuleCollider2D;
            if (capsuleCollider != null)
            {
                // Modifier le rayon en fonction de la direction de la capsule
                if (capsuleCollider.direction == CapsuleDirection2D.Horizontal)
                {
                    capsuleCollider.size = new Vector2(attackConfig.sensorRadius, capsuleCollider.size.y);
                }
                else // CapsuleDirection2D.Vertical
                {
                    capsuleCollider.size = new Vector2(capsuleCollider.size.x, attackConfig.sensorRadius);
                }
            }
            else
            {
                Debug.LogError("The collider is not a CapsuleCollider2D");
            }
        }
        private void OnEnable()
        {
            playerSensor.OnPlayerEnter += PlyaerSensorOnPlayerEnter;
            playerSensor.OnPlayerExit += PlyaerSensorOnPlayerExit;
        }
        private void OnDisable()
        {
            playerSensor.OnPlayerEnter -= PlyaerSensorOnPlayerEnter;
            playerSensor.OnPlayerExit -= PlyaerSensorOnPlayerExit;
            
        }
        private void PlyaerSensorOnPlayerExit(Vector3 lastKnownPosition)
        {
            Debug.Log("Orc Brain -- Wander Goal");
            agentBehaviour.SetGoal<WanderGoal>(true);
        }

        private void PlyaerSensorOnPlayerEnter(Transform player)
        {
            Debug.Log("Orc Brain -- Kill player goal");
            agentBehaviour.SetGoal<KillPlayerGoal>(true);
        }
    } 
}