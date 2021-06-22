using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 1.0f;
    public float jumpForce = 150;
    public float groundCheckRadius = 10;
    public int Health = 5;
    public Transform groundCheck;
    public LayerMask whatsIsGround;
    public ParticleSystem explosionRef;

    private Rigidbody2D Rigidbody2D;
    private Animator animator;
    private Vector3 LocalScale;
    private float horizontal;
    private bool isGrounded;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    private void Update()
    {   
        Jump();
        animator.SetBool("JumpOrFall", !isGrounded);
        
    }
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatsIsGround);
        Walk();
    }

    public void Walk()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal < 0.0f)
        {
            LocalScale = new Vector3(-0.3f, 0.3f, 1.0f);
            transform.localScale = LocalScale;
        }
        else if (horizontal > 0.0f)
        {
            LocalScale = new Vector3(0.3f, 0.3f, 1.0f);
            transform.localScale = LocalScale;
        }

        animator.SetBool("Walk", horizontal != 0.0f);
        Vector2 vector2 = new Vector2(movementSpeed * horizontal, Rigidbody2D.velocity.y);
        vector2 *= Time.deltaTime;
        transform.Translate(vector2);
    }

    public void Jump()
    {
        
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce);
            }
        }
    }

    public void Hit()
    {
        Health = Health - 1;
        Instantiate(explosionRef, transform.position, Quaternion.identity);
        if (Health == 0)
        {
            Destroy(gameObject, 2.0f);
            gameObject.transform.GetComponent<PlayerRespawn>().PlayerDead();
        }
    }

    public void DestroyPlayer()
    {
        Instantiate(explosionRef, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
