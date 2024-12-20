using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private Sprite fallingSprite;
    [SerializeField] private Transform groundChecker;

    [Header("Player Variables")]
    [SerializeField] private Vector2 moveDirection;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float coyoteTime;
    [SerializeField] private float coyoteTimer;
    [SerializeField] private float jumpBuffer;
    [SerializeField] private float jumpBufferTimer;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float jumpCooldownTimer;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistanceY;
    [SerializeField] private float groundCheckDistanceX;

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [Header("Unity References")]
    public UnityEvent OnLandEvent;
    [SerializeField] private InputActionReference moveRef;
    [SerializeField] private InputActionReference fireRef;  //Currently unused, here as reference to Unity's input system

    private void OnEnable()
    {
        //fireRef.action.started += Fire;
    }

    private void OnDisable()
    {
        //fireRef.action.started -= Fire;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        moveDirection = moveRef.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
        CheckWasJumping();
    }

    private void Move()
    {
        if (moveDirection.x != 0)
        {
            body.linearVelocity = new Vector2(moveDirection.x * moveSpeed, body.linearVelocityY);
            if (moveDirection.x < 0)
            {
                playerSprite.flipX = true;
            }

            else if (moveDirection.x > 0)
            {
                playerSprite.flipX = false;
            }
            //Debug.Log("Player is moving");
        }

        else
        {
            body.linearVelocity = new Vector2(0, body.linearVelocityY);
            //Debug.Log("Player is not moving");
        }

        if (Grounded())
        {
            animator.SetFloat("Speed", Mathf.Abs(moveDirection.x));
        }

        else if(isGrounded)
        {
            animator.SetFloat("Speed", 0);
        }
    }

    private void Jump()
    {

        if (moveDirection.y > 0)
        {
            jumpBufferTimer = jumpBuffer;
            jumpCooldownTimer = jumpCooldown;
        }

        else
        {
            jumpBufferTimer -= Time.deltaTime;
            jumpCooldownTimer -= Time.deltaTime;
        }

        if (coyoteTimer > 0 && jumpBufferTimer > 0 && !animator.GetBool("Jumping") && body.linearVelocity.y <= 0)
        {
            animator.SetBool("Jumping", false);
            body.linearVelocity = new Vector2(body.linearVelocityX, jumpSpeed);
            animator.SetBool("Jumping", true);
            //Debug.Log("Player is jumping");
        }

        else
        {
            coyoteTimer -= Time.deltaTime;
        }

        if (Grounded() == false && body.linearVelocity.y <= 0)
        {
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", true);
        }

        else
        {
            animator.SetBool("Falling", false);
        }
    }

    private bool Grounded()
    {
        if (Physics2D.Raycast(groundChecker.position, Vector2.down, groundCheckDistanceY, groundLayer)
            || Physics2D.Raycast(groundChecker.position + new Vector3(groundCheckDistanceX, 0, 0), Vector2.down, groundCheckDistanceY, groundLayer)
            || Physics2D.Raycast(groundChecker.position + new Vector3(-groundCheckDistanceX, 0, 0), Vector2.down, groundCheckDistanceY, groundLayer))
        {
            isGrounded = true;
            coyoteTimer = coyoteTime;
            return true;
        }

        else
        {
            isGrounded = false;
            return false;
        }
    }

    public void CheckWasJumping()
    {
        if (Grounded() && Mathf.Abs(body.linearVelocity.y) < 0.1f)
        {
            OnLandEvent.Invoke();
        }
    }

    public void OnLanding()
    {
        animator.SetBool("Jumping", false);
        animator.SetBool("Falling", false);
    }

    //Here as a reference and a example of how to do the new Input Fire System
    //private void Fire(InputAction.CallbackContext obj)
    //{
        //Debug.Log("Testing Fire Button!");
    //}
}

