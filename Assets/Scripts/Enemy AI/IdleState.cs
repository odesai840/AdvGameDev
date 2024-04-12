using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class IdleState : State
{

    public ChaseState chaseState;
    public bool canSeeThePlayer;
    public GameObject Player;
    public float playerDistance;

    void Update()
    {
        if(Player != null)
        {
            playerDistance = Vector3.Distance(this.transform.position, Player.transform.position);

            if (playerDistance < 200)
            {
                canSeeThePlayer = true;
                UnityEngine.Debug.Log("See him");
            }
            else
            {
                canSeeThePlayer = false;
            }
        }
        else
        {
            // If Player is null, we can't see it
            canSeeThePlayer = false;
        }
    }

    public override State RunCurrentState()
    {
        if (canSeeThePlayer)
        {
            return chaseState;
        }
        else
        {
            return this;
        }
    }
}
