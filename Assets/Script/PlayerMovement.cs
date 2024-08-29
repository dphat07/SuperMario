using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Camera m_camera;
    private Rigidbody2D rigidbody;

    public float moveSpeed = 8f;
   

    [SerializeField] private float jumpForce = 5f;
    private bool isGrounded = false;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float groundCheckLength = 1f;
    private bool isFacingRight = true;

    [SerializeField] CharacterAnimation characterAnimation;
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
       


    }

  

    private void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector3.down, groundCheckLength, layerMask);

        if (hit.collider == null)
        {
            isGrounded = false;
        }
        else
            isGrounded = true;
        characterAnimation.SetBool("isGrounded", groundCheck);
    }    

    private void PlayerJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = jumpForce;
            rigidbody.velocity = velocity;
            characterAnimation.SetTrigger("jump");
        }
    }

    private void FlipHander(Vector2 velocity)
    {
        Vector3 localScale = transform.localScale;
        if (isFacingRight)
        {
            if (velocity.x < 0)
            {
                isFacingRight = false;
                localScale.x = -1;
            }

        }
        else
        {
            if (velocity.x > 0)
            {
                isFacingRight = true;
                localScale.x = 1;
            }
        }
        transform.localScale = localScale;
    }

    private void HorizontalMovement()
    {
        inputAxis = Input.GetAxisRaw("Horizontal");
        Debug.Log("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime);
        velocity.y = rigidbody.velocity.y;
        rigidbody.velocity = velocity;

        characterAnimation.SetFloat("velocity",Mathf.Abs(velocity.x));
        FlipHander(velocity);
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
        {
            if (pos.x <= leftEdge.x + 0.5f)
            {
                velocity.x = 0;
                rigidbody.velocity = velocity;

            }
        }
        
        
    }
}
