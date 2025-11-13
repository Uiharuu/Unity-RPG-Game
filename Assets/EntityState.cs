using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EntityState
{
    protected StateMachine stateMachine;
    protected string animBoolName;
    protected Player player;
    protected float stateTimer;

    public EntityState(Player player, StateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Entry()
    {
        player.anim.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        player.anim.SetFloat("yVelocity", player.rb.velocity.y);
        if (player.input.Player.Dash.WasPressedThisFrame() && CanDash())
            stateMachine.ChangeState(player.dashState);
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }

    private bool CanDash()
    {
        if (stateMachine.currentState.animBoolName == "dash")
            return false;

        return true;
    }

}
