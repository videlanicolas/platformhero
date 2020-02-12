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
    bool    jump, onGround, prevGround;
    Animator animator;
    Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        onGround = true;
        prevGround = true;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        jump = Input.GetButtonDown("Jump");

        onGround = CheckGround();
        if (prevGround != onGround) {
            Debug.Log(onGround);
            prevGround = onGround;
        }
    }

    private void FixedUpdate()
    {
        if (jump && onGround) {
            onGround = false;
            rigidBody.AddForce(jumpForce * transform.up, ForceMode2D.Impulse);
        }

        rigidBody.velocity = new Vector2(horizontalMovement * speed, rigidBody.velocity.y);
        if (horizontalMovement > flipTh)
            if (gameObject.GetComponent<SpriteRenderer>().flipX) gameObject.GetComponent<SpriteRenderer>().flipX = false;
        else if (horizontalMovement < -flipTh)
            if (!gameObject.GetComponent<SpriteRenderer>().flipX) gameObject.GetComponent<SpriteRenderer>().flipX = true;
    }

    bool CheckGround() {
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, Vector2.down, 0.75f, 10);
        Debug.DrawLine((Vector2)transform.position, (Vector2)transform.position + (Vector2.down * 0.75f), Color.red, 1, false);
        if (hit.collider != null) {
            Debug.Log(hit.point);
            return true;
        }

        return false;
    }
}
