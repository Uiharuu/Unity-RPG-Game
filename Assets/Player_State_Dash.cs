using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State_Dash : EntityState
{
    private float originGravity;
    public Player_State_Dash(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Entry()
    {
        base.Entry();

        stateTimer = player.dashDuration;
        // 暂时关闭重力影响， 后续exit时恢复
        originGravity = player.rb.gravityScale;
        player.rb.gravityScale = 0;

        // 如果在墙上时先进行朝向翻转，以便于后续冲刺方向正确
        if (player.wallCheck)
            player.Flip();

        player.SetVelocity(player.rb.velocity.x, 0);

    }
    public override void Update()
    {
        base.Update();


        player.SetVelocity(player.dashSpeed * player.facingDir, player.rb.velocity.y);

        CancleDash();

        if (stateTimer < 0)
        {
            if (player.groundedCheck)
                stateMachine.ChangeState(player.idleState);
            else if (player.wallCheck)
                stateMachine.ChangeState(player.wallSlideState);
            else
                stateMachine.ChangeState(player.fallState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, 0);
        player.rb.gravityScale = originGravity;
    }

    private void CancleDash()
    {
        // 如果冲刺时碰到墙壁，取消冲刺状态
        if (player.wallCheck)
        {
            if (player.groundedCheck)
                stateMachine.ChangeState(player.idleState);
            else
            {
                Debug.Log("dash cancled");
                stateMachine.ChangeState(player.wallSlideState);

            }
        }
    }
}
