using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State_WallJump : EntityState
{
    public Player_State_WallJump(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Entry()
    {
        base.Entry();

        player.SetVelocity(player.wallJumpForce.x * -player.facingDir, player.wallJumpForce.y);
    }
    public override void Update()
    {
        base.Update();

        if (player.rb.velocity.y < 0)
            stateMachine.ChangeState(player.fallState);

        if (player.wallCheck)
            stateMachine.ChangeState(player.wallSlideState);
    }
}
