using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb { get; private set; }
    private PlayerInputSet input;

    private StateMachine stateMachine;

    public Player_State_Idle idleState { get; private set; }
    public Player_State_Move moveState { get; private set; }
    public Vector2 moveInput { get; private set; }

    [Header("Movement Settings")]
    public float moveSpeed = 8;
    private bool facingRight = true;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
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
        //moveSpeed = 8;
        //facingRight = true;
    }

    private void Update()
    {
        stateMachine.UpdateActive();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }


    private void HandleFlip(float xVelocity)
    {
        if (facingRight && xVelocity < 0)
            Flip();
        else if (!facingRight && xVelocity > 0)
            Flip();
    }
    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
    }
}
