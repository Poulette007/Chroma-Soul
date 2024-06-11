using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace Enemy.GOAP.Behaviors
{
    // , typeof(Animator)
    [RequireComponent(typeof(NavMeshAgent), typeof(AgentBehaviour))]
    public class AgentMoveBehavior : MonoBehaviour
    {
        private NavMeshAgent navMeshAgent;
        //private Animator animator;
        private AgentBehaviour AgentBehavior;
        private ITarget CurrentTarget;
        private Vector2 LastPosition;
        [SerializeField] private float MinMoveDistance = 0.25f;
        //private static readonly int WALK = Animator.StringToHash("Walk"); 

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
          //  animator = GetComponent<Animator>();
            AgentBehavior = GetComponent<AgentBehaviour>(); 

            navMeshAgent.updateRotation = false;
            navMeshAgent.updateUpAxis = false;
        }
        private void OnEnable()
        {
            AgentBehavior.Events.OnTargetChanged += EventsOnTargetChanged;
            AgentBehavior.Events.OnTargetOutOfRange += EventsOnTargetOutOfRange;
        }

        private void OnDisable()
        {
            AgentBehavior.Events.OnTargetChanged -= EventsOnTargetChanged;
            AgentBehavior.Events.OnTargetOutOfRange -= EventsOnTargetOutOfRange;
        }

        private void EventsOnTargetOutOfRange(ITarget target)
        {
            //animator.SetBool(WALK, false);
        }

        private void EventsOnTargetChanged(ITarget target, bool inRange)
        {
            CurrentTarget = target;
            Debug.Log("Position target: " + CurrentTarget.Position); 
            Debug.Log("Position Self: " + AgentBehavior.transform.position); 
            LastPosition = CurrentTarget.Position;
            navMeshAgent.SetDestination(target.Position);
            //animator.SetBool(WALK, true);
        }

        private void Update()
        {
            if (CurrentTarget == null)
            {
                Debug.Log("Target  null ");
                return;
            }

            if (MinMoveDistance <= Vector2.Distance(CurrentTarget.Position, LastPosition))
            {
                Debug.Log("Target : " + CurrentTarget + "   Pos: " + CurrentTarget.Position);
                LastPosition = CurrentTarget.Position;
                navMeshAgent.SetDestination(CurrentTarget.Position);    
            }
            
            //animator.SetBool(WALK, navMeshAgent.velocity.magnitude > 0.1f);
        }
    }
}