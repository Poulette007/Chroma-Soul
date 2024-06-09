using CrashKonijn.Goap.Behaviours;
using Enemy.GOAP.Goals;
using UnityEngine;

namespace Enemy.GOAP.Behaviors
{
    [RequireComponent(typeof(AgentBehaviour))]
    public class OrcBrain : MonoBehaviour
    {
        private AgentBehaviour agentBehaviour;

        private void Awake()
        {
            agentBehaviour = GetComponent<AgentBehaviour>();
        }
        private void Start()
        {
            agentBehaviour.SetGoal<WanderGoal>(false);
        }
    } 
}