using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player_State_BasicAttack : EntityState
{
    private float attackDuration;
    private int comboAttackIndex = 0;
    private int totalComboAttacks = 3;
    private float lastAttackTime;
    private const int FirstComboIndex = 0;
    private int attackDir;

    private bool comboAttackQueue;
    public Player_State_BasicAttack(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Entry()
    {
        base.Entry();
        comboAttackQueue = false;
        ResetCombo();
        player.anim.SetInteger("basicAttackIndex", comboAttackIndex);
        // 根据输入方向决定攻击方向， 无输入时使用面向方向
        attackDir = player.moveInput.x != 0 ? (int)player.moveInput.x : player.facingDir;

        AttackFeedBack();
    }
    public override void Update()
    {
        base.Update();


        if (player.input.Player.Attack.WasPressedThisFrame())
        {
            comboAttackQueue = true;
        }

        // 攻击时速度归零， 避免滑动
        HandleAttackVelocity();

        // 通过触发器切换回idle状态
        // 该触发器应在animator的脚本中设置， 并且在动画事件最后一帧触发
        if (stateTrigger)
        {
            HandleExit();
        }            
        
    }

    public override void Exit()
    {
        base.Exit();
        lastAttackTime = Time.time;
        comboAttackIndex = (comboAttackIndex + 1) % 3;

    }
    // 处理状态退出逻辑
    // 如果连招被触发且未到达连招末尾， 则进入下一个连招
    private void HandleExit()
    {
        if (comboAttackQueue && comboAttackIndex != totalComboAttacks - 1)
        {
            player.anim.SetBool(animBoolName, false);
            player.EnterAttackStateWithDelay();
        }
        else
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
    
    private void AttackFeedBack()
    {
        attackDuration = player.attackDuration;

        if (comboAttackIndex >= player.attackMove.Length)
        {
            Debug.LogWarning("Combo Attack Index out of range");
            return;
        }
        player.SetVelocity(player.attackMove[comboAttackIndex].x * attackDir, player.attackMove[comboAttackIndex].y);
    }

    private void ResetCombo()
    {
        if (Time.time - lastAttackTime > player.comboAttackInterval)
        {
            comboAttackIndex = FirstComboIndex;
        }
    }
}
