using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb { get; private set; }
    public PlayerInputSet input { get; private set; }

    private StateMachine stateMachine;

    public Player_State_Idle idleState { get; private set; }
    public Player_State_Move moveState { get; private set; }
    public Player_State_Jump jumpState { get; private set; }
    public Player_State_Fall fallState { get; private set; }
    public Player_State_WallJump wallJump { get; private set; }
    public Player_State_WallSlide wallSlideState { get; private set; }
    public Player_State_Dash dashState { get; private set; }

    public Vector2 moveInput { get; private set; }

    [Header("Movement Settings")]
    public float moveSpeed = 8;
    public float jumpForce = 12;
    private bool facingRight = true;
    public int facingDir { get; private set; } = 1;
    public Vector2 wallJumpForce = new Vector2(6, 12);
    public float dashDuration { get; private set; } = .25f;
    public float dashSpeed { get; private set; } = 20f;
    [SerializeField] public float wallJumpDuration = .4f;

    [Header("Collision Detection")]
    [SerializeField] float groundCheckDistance = 1.35f;
    [SerializeField] float wallCheckDistance = 0.45f;
    [SerializeField] LayerMask whatIsGround;
    public bool groundedCheck;
    public bool wallCheck;


    public float wallSlideFallMultiper = .4f;
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        input = new PlayerInputSet();

        stateMachine = new StateMachine();
        idleState = new Player_State_Idle(this, stateMachine, "idle");
        moveState = new Player_State_Move(this, stateMachine, "move");
        jumpState = new Player_State_Jump(this, stateMachine, "jumpFall");
        fallState = new Player_State_Fall(this, stateMachine, "jumpFall");
        wallSlideState = new Player_State_WallSlide(this, stateMachine, "wallSlide");
        wallJump = new Player_State_WallJump(this, stateMachine, "jumpFall");
        dashState = new Player_State_Dash(this, stateMachine, "dash");
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
        HandleCollisionDetaction();
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
    private void HandleCollisionDetaction()
    {
        groundedCheck = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallCheck = Physics2D.Raycast(transform.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
    }

    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
        facingDir = facingDir * -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(wallCheckDistance * facingDir, 0));
    }
}
