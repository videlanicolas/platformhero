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
    public AudioClip jumpSound;
    public bool godMode = false;

    float   horizontalMovement;
    bool    jump, onGround, prevGround, won;
    Animator animator;
    Rigidbody2D rigidBody;
    CircleCollider2D circleColider;
    AudioSource audioSource;
    float leftLimit, rightLimit;
    GameObject levelController;

    // Start is called before the first frame update
    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        circleColider = gameObject.GetComponent<CircleCollider2D>();
        audioSource = gameObject.GetComponent<AudioSource>();
        levelController = GameObject.FindGameObjectWithTag("LevelController");
        onGround = true;
        prevGround = true;
        jump = false;
        won = false;

        leftLimit = GameObject.FindGameObjectWithTag("LeftWall").transform.position.x;
        rightLimit = GameObject.FindGameObjectWithTag("RightWall").transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is dead, we don't have to take any other input.
        if (animator.GetBool("Dead") || won) return;
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
            audioSource.PlayOneShot(jumpSound);
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

        // Clamp position to the start and end of the level, so the hero doesn't fall.
        transform.position = (new Vector3(Mathf.Clamp(transform.position.x, leftLimit, rightLimit), transform.position.y));
    }

    bool CheckGround() {
        float extraY = 0.1f;
        RaycastHit2D hit = Physics2D.Raycast((Vector2)circleColider.bounds.center, Vector2.down, circleColider.bounds.extents.y + extraY, groundLayer);
        if (hit.collider != null) {
            return true;
        }
        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // We use CompareTag instead of aswitch statement because CompareTag is faster than getting the tag.
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            Dead();
            return;
        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Dead();
            // Because we have to destroy the Bullet, we need to duplicate the logic.
            Destroy(collision.gameObject);
            return;
        }
    }

    // Called when the player is dead.
    private void Dead()
    {
        if (godMode) return;
        transform.Rotate(0, 0, 90);
        animator.SetBool("Dead", true);
        horizontalMovement = 0;

        // Let all enemies know that the player is dead.
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemy.GetComponent<EnemyAI>().PlayerDead();
        }

        levelController.GetComponent<LevelController>().Dead();
        rigidBody.AddForce(4 * Vector2.up, ForceMode2D.Impulse);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }

    public void Won()
    {
        won = true;
        horizontalMovement = 1f;
    }
}
