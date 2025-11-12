using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EntityState
{
    protected StateMachine stateMachine;
    protected string stateName;

    public EntityState(StateMachine stateMachine, string stateName)
    {
        this.stateMachine = stateMachine;
        this.stateName = stateName;
    }

    public virtual void Entry()
    {
        Debug.Log("Entry: " + stateName);
    }

    public virtual void Update()
    {
        Debug.Log("Updata: " + stateName);
    }

    public virtual void Exit()
    {
        Debug.Log("Exit: " + stateName);
    }
}
