using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State_Move : EntityState
{
    public Player_State_Move(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (player.moveInput.x == 0)
            stateMachine.ChangeState(player.idleState);

        player.SetVelocity(player.moveInput.x * player.moveSpeed, player.rb.velocity.y);

    }


}
