using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public GameObject enemyID;
    public abstract State RunCurrentState();

}
