using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    [Range(0.1f, 10f)]
    public float speed;
    [Range(1f, 20f)]
    public float jumpForce;

    float   flipTh = 0.01f,
            horizontalMovement;
    bool    jump, right, onGround;
    Animator animator;
    Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        right = true;
        onGround = true;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        jump = Input.GetButtonDown("Jump");
    }

    private void FixedUpdate()
    {
        if (jump && onGround) {
            Debug.Log("Jump!");
            // onGround = false;
            rigidBody.AddForce(jumpForce * transform.up, ForceMode2D.Impulse);
        }

        rigidBody.velocity = new Vector2(horizontalMovement * speed, rigidBody.velocity.y);
        Debug.Log(rigidBody.velocity);
        // rigidBody.MovePosition(new Vector2(rigidBody.position.x + horizontalMovement * speed, rigidBody.position.y));
        if (horizontalMovement > flipTh)
        {
            if (!right)
            {
                right = true;
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }

        }
        else if (horizontalMovement < -flipTh) {
            if (right)
            {
                right = false;
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        
    }


}
