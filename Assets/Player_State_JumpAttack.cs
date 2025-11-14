using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State_JumpAttack : EntityState
{
    private bool jumpAttackDone;
    public Player_State_JumpAttack(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Entry()
    {
        base.Entry();

        jumpAttackDone = false;
        player.SetVelocity(player.jumpAttackVelocity.x * player.facingDir, player.jumpAttackVelocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (player.groundedCheck && !jumpAttackDone)
        {
            jumpAttackDone = true;
            player.anim.SetTrigger("jumpAttackTrigger");
            player.SetVelocity(0, 0);
        }

        if (stateTrigger && player.groundedCheck)
        {
            stateMachine.ChangeState(player.idleState);
        }
            
    }


}
