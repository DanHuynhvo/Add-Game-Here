using Mono.Cecil;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D body;
    [SerializeField]
    private InputActionReference moveRef;
    [SerializeField]
    private InputActionReference fireRef;
    [SerializeField]
    private Vector2 moveDirection;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private Transform groundChecker;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private float groundCheckDistanceY;
    [SerializeField]
    private float groundCheckDistanceX;

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
    }

    private void Move()
    {
        if (moveDirection.x != 0)
        {
            body.linearVelocity = new Vector2(moveDirection.x * moveSpeed, body.linearVelocityY);
            //Debug.Log("Player is moving");
        }

        else
        {
            body.linearVelocity = new Vector2(0, body.linearVelocityY);
            //Debug.Log("Player is not moving");
        }
    }

    private void Jump()
    {
        if (Grounded() == true && moveDirection.y > 0)
        {
            body.linearVelocity = new Vector2(body.linearVelocityX, jumpSpeed);
            //Debug.Log("Player is jumping");
        }
    }

    private bool Grounded()
    {
        if (Physics2D.Raycast(groundChecker.position, Vector2.down, groundCheckDistanceY, groundLayer)
            || Physics2D.Raycast(groundChecker.position + new Vector3(groundCheckDistanceX, 0, 0), Vector2.down, groundCheckDistanceX, groundLayer)
            || Physics2D.Raycast(groundChecker.position + new Vector3(-groundCheckDistanceX, 0, 0), Vector2.down, groundCheckDistanceX, groundLayer))
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    //Here as a reference and a example of how to do the new Input Fire System
    /*private void Fire(InputAction.CallbackContext obj)
    {
        Debug.Log("Testing Fire Button!");
    }*/
}
