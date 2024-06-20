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
        [SerializeField] private AreaSensor areaSensor;
        [SerializeField] private BotActionConfigSO botActionConfig;
        [SerializeField] public StatsConfigSO statsConfig;

        private AgentBehaviour agentBehaviour;
        private AgentMoveBehavior agentMoveBehavior;

        private void Awake()
        {
            agentBehaviour = GetComponent<AgentBehaviour>();
            agentMoveBehavior = GetComponent<AgentMoveBehavior>();
        }
        private void Start()
        {
            if (!botActionConfig.isProtectingArea)
            {
                agentBehaviour.SetGoal<WanderGoal>(false);
                UpdateSensorColliderSize();
            }
            else
            {
                botActionConfig.CenterPosition =  areaSensor.transform.position;
                agentBehaviour.SetGoal<KillPlayerGoal>(false);
                agentBehaviour.SetGoal<WanderGoal>(false);
                agentBehaviour.SetGoal<ProtectAreaGoal>(true);
                UpdateSensorColliderSize();
            }
            
        }
        private void OnEnable()
        {
            if  (!botActionConfig.isProtectingArea)
            {
                playerSensor.OnPlayerEnter += PlayerSensorOnPlayerEnter;
                playerSensor.OnPlayerExit += PlayerSensorOnPlayerExit;
            }
            else if (botActionConfig.isProtectingArea)
            {
                areaSensor.OnPlayerEnter += PlayerSensorOnPlayerEnter;
                areaSensor.OnPlayerExit += PlayerSensorOnPlayerExit;
            }               
        }
        private void OnDisable()
        {
            if (!botActionConfig.isProtectingArea)
            {
                playerSensor.OnPlayerEnter -= PlayerSensorOnPlayerEnter;
                playerSensor.OnPlayerExit -= PlayerSensorOnPlayerExit;
            }
            else if (botActionConfig.isProtectingArea)
            {
                areaSensor.OnPlayerEnter -= PlayerSensorOnPlayerEnter;
                areaSensor.OnPlayerExit -= PlayerSensorOnPlayerExit;
            } 
        }
        private void PlayerSensorOnPlayerExit(Vector3 lastKnownPosition)
        {
            if (!botActionConfig.isProtectingArea)
            {
                agentBehaviour.SetGoal<WanderGoal>(true);
            }
            else 
            {
                botActionConfig.isPlayerInArea = false;
                botActionConfig.ChangeAction = true;
                agentBehaviour.SetGoal<ProtectAreaGoal>(true);
            }
        }

        private void PlayerSensorOnPlayerEnter(Transform player)
        {
            if (!botActionConfig.isProtectingArea)
            {
                agentBehaviour.SetGoal<KillPlayerGoal>(true);
            }
            else
            {
                botActionConfig.isPlayerInArea = true;
                botActionConfig.ChangeAction = true;
                agentBehaviour.SetGoal<ProtectAreaGoal>(true);
            }
        }
        public void OnBossDetected()
        {
            agentBehaviour.SetGoal<KillPlayerGoal>(false);
            agentBehaviour.SetGoal<WanderGoal>(false);
            agentBehaviour.SetGoal<ProtectAreaGoal>(false);
        }

        private void UpdateSensorColliderSize()
        {
            var capsuleCollider = playerSensor.Collider as CapsuleCollider2D;
            if (capsuleCollider != null)
            {
                if (capsuleCollider.direction == CapsuleDirection2D.Horizontal)
                {
                    capsuleCollider.size = new Vector2(botActionConfig.sensorRadius, capsuleCollider.size.y);
                }
                else
                {
                    capsuleCollider.size = new Vector2(capsuleCollider.size.x, botActionConfig.sensorRadius);
                }
            }
            else
            {
                Debug.LogError("The collider is not a CapsuleCollider2D");
            }
        }
        public void SetTarget(Vector2 targetPosition)
        {
            agentMoveBehavior.MoveTo(targetPosition);
        }
    } 
}