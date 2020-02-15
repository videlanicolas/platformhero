using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    [Range(0.1f, 10f)]
    public float speed;
    [Range(1f, 20f)]
    public float jumpForce;
    [SerializeField]
    public LayerMask groundLayer;

    float   horizontalMovement;
    bool    jump, onGround, prevGround;
    Animator animator;
    Rigidbody2D rigidBody;
    BoxCollider2D boxCollider;
    // Start is called before the first frame update
    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        onGround = true;
        prevGround = true;
        jump = false;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Velocity", Mathf.Abs(horizontalMovement));
        // Update runs with every frame, we get the "Jump" input here because it's the fastest way posible.
        // FixedUpdate will check for "Jump", that's why we need to save the state of "Jump" until it's consumed by FixedUpdate.
        // This leads to no input loss on one-time key presses.
        if (!jump && onGround) jump = Input.GetButtonDown("Jump");
        onGround = CheckGround();
        animator.SetBool("OnGround", onGround);
        if (prevGround != onGround) {
            Debug.Log("onGround changed: " + onGround);
            prevGround = onGround;
        }
    }

    private void FixedUpdate()
    {
        if (jump && onGround) {
            onGround = false;
            // Since the physics engine did it's thing, we consume the jump button and set it again to false.
            // This allows Update to assign the bool value again.
            // jump = false;
            jump = false;
            rigidBody.AddForce(jumpForce * transform.up, ForceMode2D.Impulse);
        }

        rigidBody.velocity = new Vector2(horizontalMovement * speed, rigidBody.velocity.y);
        if (horizontalMovement > 0)
        {
            if (gameObject.GetComponent<SpriteRenderer>().flipX) gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (horizontalMovement < 0)
        {
            if (!gameObject.GetComponent<SpriteRenderer>().flipX) gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    bool CheckGround() {
        float extraY = 0.1f;
        RaycastHit2D hit = Physics2D.Raycast((Vector2)boxCollider.bounds.center, Vector2.down, boxCollider.bounds.extents.y + extraY, groundLayer);
        if (hit.collider != null) {
            return true;
        }
        return false;
    }
}
