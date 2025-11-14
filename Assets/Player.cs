using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb { get; private set; }
    public PlayerInputSet input { get; private set; }

    public StateMachine stateMachine;

    public Player_State_Idle idleState { get; private set; }
    public Player_State_Move moveState { get; private set; }
    public Player_State_Jump jumpState { get; private set; }
    public Player_State_Fall fallState { get; private set; }
    public Player_State_WallJump wallJump { get; private set; }
    public Player_State_WallSlide wallSlideState { get; private set; }
    public Player_State_Dash dashState { get; private set; }
    public Player_State_BasicAttack basicAttackState { get; private set; }
    public Player_State_JumpAttack jumpAttackState { get; private set; }

    public Vector2 moveInput { get; private set; }

    [Header("Movement Settings")]
    public float moveSpeed = 8;
    public float jumpForce = 12;
    private bool facingRight = true;
    public int facingDir { get; private set; } = 1;
    public Vector2 wallJumpForce = new Vector2(6, 12);
    public float dashDuration { get; private set; } = .25f;
    public float dashSpeed { get; private set; } = 20f;
    [SerializeField] public float wallJumpDuration = .2f;

    [Header("Collision Detection")]
    [SerializeField] float groundCheckDistance = 1.35f;
    [SerializeField] float wallCheckDistance = 0.45f;
    [SerializeField] Transform headWallCheckPosition;
    [SerializeField] Transform footWallCheckPosition;
    [SerializeField] LayerMask whatIsGround;
    public bool groundedCheck;
    public bool wallCheck;

    [Header("Attack Settings")]
    public Vector2[] attackMove = new Vector2[]
    {
        new Vector2(3, 1.5f),
        new Vector2(1, 2.5f),
        new Vector2(4, 5)
    };
    public float attackDuration = .1f;
    public float comboAttackInterval = .5f;
    public Coroutine enterAttackQueue;
    public Vector2 jumpAttackVelocity = new Vector2(5, -6);

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
        basicAttackState = new Player_State_BasicAttack(this, stateMachine, "basicAttack");
        jumpAttackState = new Player_State_JumpAttack(this, stateMachine, "jumpAttack");

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

    public void EnterAttackStateWithDelay()
    {
        if (enterAttackQueue != null)
            StopCoroutine(enterAttackQueue);
        StartCoroutine(EnterAttackStateWithDelayCo());
    }
    // 使用协程等待一帧， 确保animator的bool参数被正确重置
    private IEnumerator EnterAttackStateWithDelayCo()
    {
        yield return new WaitForEndOfFrame();
        stateMachine.ChangeState(basicAttackState);
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
        wallCheck = Physics2D.Raycast(headWallCheckPosition.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround)
                    && Physics2D.Raycast(footWallCheckPosition.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
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
        Gizmos.DrawLine(headWallCheckPosition.position, headWallCheckPosition.position + new Vector3(wallCheckDistance * facingDir, 0));
        Gizmos.DrawLine(footWallCheckPosition.position, footWallCheckPosition.position + new Vector3(wallCheckDistance * facingDir, 0));
    }
}
