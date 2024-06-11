using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public State CurrentState;
    [SerializeField] private GameObject enemyID;
    void Update()
    {
        RunStateMachine();
    }
    void Start()
    {
        // Récupère l'objet ennemi à partir du GameObject parent
        enemyID = gameObject;
        CurrentState.enemyID = enemyID;
    }

    private void RunStateMachine()
    {
        State nextState = CurrentState?.RunCurrentState();

        if (nextState != CurrentState)  
        {
            SwitchToTheNextState(nextState);
        }
    }
    private void SwitchToTheNextState(State nextState)
    {
        
        Debug.Log("STATE MANAGER -- is enter in : "  + nextState.ToString() + "  Enemy name : " + enemyID.name);
        nextState.enemyID = enemyID; 
        CurrentState = nextState;
    }
}
