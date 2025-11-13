using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State_Idle : Player_State_Grounded
{
    public Player_State_Idle(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Entry()
    {
        base.Entry();

        player.SetVelocity(0, player.rb.velocity.y);
    }
    public override void Update()
    {
        base.Update();

        if (player.moveInput.x != 0)
            stateMachine.ChangeState(player.moveState);
    }
}
