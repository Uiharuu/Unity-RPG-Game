using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State_Idle : EntityState
{
    public Player_State_Idle(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (player.moveInput.x != 0)
            stateMachine.ChangeState(player.moveState);
    }
}
