using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State_Jump : Player_State_Aired
{
    public Player_State_Jump(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Entry()
    {
        base.Entry();

        player.SetVelocity(player.rb.velocity.x, player.jumpForce);
    }

    public override void Update()
    {
        base.Update();

        if (player.rb.velocity.y < 0)
            stateMachine.ChangeState(player.fallState);
    }
}
