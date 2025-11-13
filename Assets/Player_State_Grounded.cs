using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State_Grounded : EntityState
{
    public Player_State_Grounded(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();
        if (player.rb.velocity.y < 0)
            stateMachine.ChangeState(player.fallState);

        if (player.input.Player.Jump.WasPressedThisFrame())
            stateMachine.ChangeState(player.jumpState);
    }
}
