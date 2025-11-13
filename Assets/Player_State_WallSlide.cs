using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State_WallSlide : EntityState
{
    public Player_State_WallSlide(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();
        HandleSlide();

        if (player.input.Player.Jump.WasPressedThisFrame())
            stateMachine.ChangeState(player.wallJump);

        if (player.groundedCheck)
            stateMachine.ChangeState(player.idleState);

        if (!player.wallCheck)
            stateMachine.ChangeState(player.fallState);
    }

    private void HandleSlide()
    {
        if (player.moveInput.y < 0)
        {
            player.SetVelocity(player.moveInput.x, player.rb.velocity.y);
        }
        else
        {
            player.SetVelocity(player.moveInput.x, player.rb.velocity.y * player.wallSlideFallMultiper);
        }
    }
}
