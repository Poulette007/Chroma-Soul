using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int Health = 1000;

        public void OnDamage(int damage)
        {
            Health -= damage;
            // also handle death and stuff :)
        }
}
