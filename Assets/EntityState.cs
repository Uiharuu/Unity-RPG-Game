using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EntityState
{
    protected StateMachine stateMachine;
    protected string animBoolName;
    protected Player player;

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
        Debug.Log("Updata: " + animBoolName);
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }
}
