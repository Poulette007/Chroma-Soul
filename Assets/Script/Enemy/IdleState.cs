using System.Collections;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class IdleState : State
{
    public ChaseState chaseState;
    public bool canSeeThePlayer = false;
    [SerializeField] private float speed = 2f;
    private Vector2 direction;

    [SerializeField] private float visionRange = 6f; 
    [SerializeField] private Transform playerTransform;

    private float changeDirectionTime;

    private void Update()
    {
        changeDirectionTime -= Time.deltaTime;
    }
    public override State RunCurrentState()
    {
        
        if (canSeeThePlayer)
        {
            Debug.Log("Idle State -- switch to " + chaseState.name);
            chaseState.canSeeThePlayer = true;
            return chaseState;
        }
        else
        {
            StartCoroutine(PerformIdleActions());
            return this;
        }
    }

    private IEnumerator  PerformIdleActions()
    {
        Rigidbody2D rb = enemyID.GetComponent<Rigidbody2D>();
        
        while (!canSeeThePlayer)
        {
            rb.MovePosition(rb.position + direction * (speed * Time.fixedDeltaTime));
            CheckForPlayer();

            if(canSeeThePlayer)
            {
                yield break;
            }
            if(changeDirectionTime <= 0.0f)
            {
                direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized;
                changeDirectionTime = speed;
            }
            yield return new WaitForFixedUpdate();
        }
    }
    private void CheckForPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer < visionRange)
        {
            canSeeThePlayer = true;
        }
        else
        {
            canSeeThePlayer = false;
        }
    }   
}

