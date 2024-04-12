using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    
    public override State RunCurrentState()
    {
        Debug.Log("I Have Attacked");
        return this;
    }
}
