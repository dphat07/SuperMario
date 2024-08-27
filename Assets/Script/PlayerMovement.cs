using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Camera m_camera;
    private Rigidbody2D rigidbody;

    public float moveSpeed = 8f;
    //public float maxJumpHeight = 5f;
    //public float maxJumpTime = 1f;
    //public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
    //public float gravity => (-2f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2f), 2);

    //public bool grounded { get; private set; }
    //public bool jumping { get; private set; }

    [SerializeField] private float jumpForce = 5f;
    private bool isGrounded = false;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float groundCheckLength = 1f;

    private float inputAxis;
    private Vector2 velocity;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        m_camera = Camera.main;
    }

    private void Update()
    {
        HorizontalMovement();
        GroundCheck();
        PlayerJump();
        //grounded = rigidbody.Raycast(Vector2.down);
        //if (grounded)
        //{
        //    GroundedMovement();

        //}
        //ApplyGravity();


    }

    //private void GroundedMovement()
    //{
    //    velocity.y = Mathf.Max(velocity.y, 0);
    //    jumping = velocity.y > 0f;
    //    if (Input.GetButtonDown("Jump"))
    //    {
    //        Debug.Log("Jump");
    //        velocity.y = jumpForce;
    //        jumping = true;
    //    }    
    //}    

    //private void ApplyGravity()
    //{
    //    bool falling = velocity.y < 0f || !Input.GetButton("Jump");
    //    float multiplayer = falling ? 2f : 1f;

    //    velocity.y += gravity * multiplayer * Time.deltaTime;
    //    velocity.y = Mathf.Max(velocity.y, gravity / 2f);
    //}    

    private void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector3.down, groundCheckLength, layerMask);

        if (hit.collider == null)
        {
            isGrounded = false;
        }
        else
            isGrounded = true;
    }    

    private void PlayerJump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpForce;
            rigidbody.velocity = velocity;  
        }
    }    

    private void HorizontalMovement()
    {
        inputAxis = Input.GetAxisRaw("Horizontal");
        Debug.Log("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime);
        velocity.y = rigidbody.velocity.y;
        rigidbody.velocity = velocity;
    }

    private void FixedUpdate()
    {
        Vector2 pos = rigidbody.position;
       

        Vector2 leftEdge = m_camera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = m_camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        if (velocity.x > 0)
        {
            if (pos.x >= rightEdge.x + 0.5f)
            {
                velocity.x = 0;
                rigidbody.velocity = velocity;

            }
        }
        if (velocity.x < 0)
        {s
            if (pos.x <= leftEdge.x + 0.5f)
            {
                velocity.x = 0;
                rigidbody.velocity = velocity;

            }
        }
        
        
    }
}
