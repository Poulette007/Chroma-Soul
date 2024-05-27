using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{
    public AttackState attackState;

    public IdleState idleState;
    public bool isInAttackRange;
    [SerializeField] Transform target; 
    [SerializeField] private float speed = 2f;
    NavMeshAgent agent; 
    public bool canSeeThePlayer;

    private void Start()
    {
        agent = enemyID.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = speed;
        agent.nextPosition = enemyID.GetComponent<Rigidbody2D>().position;
    }
    public override State RunCurrentState()
    {   
        if (isInAttackRange)
        {
            return attackState;
        }
        else if (!canSeeThePlayer)
        {
            Debug.Log("Chase State : can not see player");
            return idleState;
        }
        else
        {
            StartCoroutine(PerformIdleActions());
            return this;
        }
    }
      private IEnumerator  PerformIdleActions()
    {
        //Rigidbody2D rb = enemyID.GetComponent<Rigidbody2D>();
        while (canSeeThePlayer)
        {
            agent.SetDestination(target.position);
            yield return new WaitForFixedUpdate();
            
        }
    }
}
