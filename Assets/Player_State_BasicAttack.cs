using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State_BasicAttack : EntityState
{
    private float attackDuration;
    public Player_State_BasicAttack(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Entry()
    {
        base.Entry();
        HandleAttackMove();
    }
    public override void Update()
    {
        base.Update();
        // 攻击时速度归零， 避免滑动
        HandleAttackVelocity();
        // 通过触发器切换回idle状态
        // 该触发器应在animator的脚本中设置， 并且在动画事件最后一帧触发
        if (stateTrigger)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    private void HandleAttackVelocity()
    {
        attackDuration -= Time.deltaTime;
        if (attackDuration < 0)
            player.SetVelocity(0, player.rb.velocity.y);
    }

    private void HandleAttackMove()
    {
        attackDuration = player.attackDuration;

        player.SetVelocity(player.attackMove.x * player.facingDir, player.attackMove.y);
    }
}
