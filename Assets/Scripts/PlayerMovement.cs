using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D coll;
    [SerializeField] private Animator squashScretch;
    [SerializeField] private ParticleSystem jumpPS;

    [SerializeField] private float moveSpeed;

    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityScale;
    [SerializeField] private float fallGravityScale;
    [SerializeField] private LayerMask jumpableGround;

    private float horizontalInput;
    bool isFacingRight = true;

    enum MovementState { idle, walk, jump, fall, land}

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpParticle();
        }
        //havan düþerken hýzlansýn fallgracity fazla
        if(rb.velocity.y > 0)
        {
            rb.gravityScale = gravityScale;
        }
        else
        {
            rb.gravityScale = fallGravityScale;
        }

        UpdateAnimation();

    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector2.down, .1f, jumpableGround);

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed * Time.deltaTime, rb.velocity.y);
    }


    private void UpdateAnimation()
    {
        MovementState state;
        if(horizontalInput > 0f && !isFacingRight)
        {
            Flip();
            state = MovementState.walk;
        }
        else if(horizontalInput < 0f && isFacingRight)
        {
            Flip();
            state = MovementState.walk;
        }
        else
        {
            state = MovementState.idle;
        }

        if(rb.velocity.y > .1f)
        {
            state = MovementState.jump;
            
        }
        else if(rb.velocity.y < -.1f)
        {
            state = MovementState.fall;
        }

        if(IsGrounded())
        {
            state = MovementState.land;
        }

        squashScretch.SetInteger("state", (int)state);
    }

    void jumpParticle()
    {
        jumpPS.Play();
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }
}
