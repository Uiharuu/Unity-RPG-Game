using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim;
    private PlayerInputSet input;

    private StateMachine stateMachine;

    public Player_State_Idle idleState { get; private set; }
    public Player_State_Move moveState { get; private set; }
    public Vector2 moveInput { get; private set; }

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        input = new PlayerInputSet();

        stateMachine = new StateMachine();
        idleState = new Player_State_Idle(this, stateMachine, "idle");
        moveState = new Player_State_Move(this, stateMachine, "move");
    }

    private void OnEnable()
    {
        input.Enable();

        input.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Movement.canceled += ctx => moveInput = ctx.ReadValue<Vector2>();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void Start()
    {
        stateMachine.Init(idleState);
    }

    private void Update()
    {
        stateMachine.UpdateActive();
    }


}
