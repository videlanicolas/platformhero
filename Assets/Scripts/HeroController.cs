using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    [Range(0.01f, 0.1f)]
    public float speed = 0.1f;
    [Range(1f, 20f)]
    public float jumpForce = 1f;

    float   flipTh = 0.01f,
            horizontalMovement;
    bool    jump, right;
    Animator animator;
    Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        right = true;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        jump = Input.GetButtonDown("Jump");
    }

    private void FixedUpdate()
    {
        rigidBody.MovePosition(new Vector2(rigidBody.position.x + horizontalMovement * speed, rigidBody.position.y));
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
