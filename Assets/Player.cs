using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private StateMachine stateMachine;

    private EntityState idleState;

    private void Awake()
    {
        stateMachine = new StateMachine();
        idleState = new EntityState(stateMachine, "idle State");
    }

    private void Start()
    {
        stateMachine.Init(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
    }
}
