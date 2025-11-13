using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State_Aired : EntityState
{
    public Player_State_Aired(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();
   

        if (player.moveInput.x != 0)
            player.SetVelocity(player.moveInput.x * player.moveSpeed, player.rb.velocity.y);

        if (player.wallCheck)
            stateMachine.ChangeState(player.wallSlideState);
    }
}
