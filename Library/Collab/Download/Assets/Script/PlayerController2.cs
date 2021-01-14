using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController2 : MonoBehaviour
{
    private enum State {idle, running, jumping, falling, hurt}
    private State state = State.idle;

    public Rigidbody2D rb;
    public Animator anim;
    private Collider2D coll;

    [SerializeField] private bool powerup = false;
    [SerializeField] private float speed = 9f;
    [SerializeField] private float jumpforce = 10f;
    [SerializeField] private LayerMask ground;
    



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        rb.velocity = new Vector2(10, rb.velocity.y);
    }

    private void Update()
    {
        if (state != State.hurt)
        {
            Moving();
        }
        VelocityState();
        anim.SetInteger("State", (int)state);
        print(state);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Bird bird = other.gameObject.GetComponent<Bird>();
            if (state == State.falling)
            { 
                bird.keinjek();
                Jump();
            }
            else
            {
                state = State.hurt;
                if(other.gameObject.transform.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2(-10, 10);
                    
                }
                else
                {
                    rb.velocity = new Vector2(10, 10);
                    transform.localScale = new Vector2(-1, 1);
                }
                //die
            }
        }
    }

    private void Moving()
    {
        float xDirection = Input.GetAxis("Horizontal");
        float yDirection = Input.GetAxis("Vertical");
        if(state != State.hurt)
        {
            if (xDirection < 0)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                transform.localScale = new Vector2(-1, 1);
            }
            else if (xDirection > 0)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                transform.localScale = new Vector2(1, 1);
            }
            if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
            {
                Jump();
            }
        }        
    }
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpforce);
        state = State.jumping;
    }
    private void VelocityState()
    {
        if(state == State.jumping)
        {
            if(rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if (Mathf.Abs(rb.velocity.x) < Mathf.Epsilon)
            {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > .1f)
        {
                state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }
}
