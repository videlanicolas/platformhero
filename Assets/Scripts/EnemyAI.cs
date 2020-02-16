using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIType {
    None,
    Patrol,
    Shoot,
}

delegate void AImethod();

public class EnemyAI : MonoBehaviour
{
    [Range(0f, 10f)]
    public float speed;
    public GameObject player;
    [Range(0f, 10f)]
    public float detectionLength;
    public AIType AItype = AIType.None;
    [Range(0f, 10f)]
    public float endRoute;
    public GameObject bullet;
    public Vector2 bulletInstantiateVector;
    [Range(0f, 5f)]
    public float timeToShoot;
    [Range(0f, 1f)]
    public float bulletSpeed;

    Rigidbody2D rigidBody;
    Animator animator;
    SpriteRenderer spriteRenderer;
    float horizontalMovement, startRoute, timer;
    AImethod method;
    bool playerDead;

    const float lightWalk = 0.1f,
                fastWalk = 0.5f,
                run = 1f;

    // Method called by Player once it's dead.
    public void PlayerDead()
    {
        playerDead = true;
    }

    private void Awake()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        animator = gameObject.GetComponent<Animator>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        // We do this once and load the method that needs to execute on Update.
        switch (AItype) {
            case AIType.Patrol:
                method = delegate() { PatrolAI(); };
                break;
            case AIType.Shoot:
                method = delegate () { ShootAI(); };
                break;
            default:
                break;
        }

        playerDead = false;
        timer = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        horizontalMovement = lightWalk;
        startRoute = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDead) return;
        animator.SetFloat("Velocity", Mathf.Abs(horizontalMovement));
        // Call the method that executes the AI.
        method();
        if (animator.GetBool("PointGun")) timer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(horizontalMovement * speed, rigidBody.velocity.y);
        if (horizontalMovement < 0) spriteRenderer.flipX = false;
        else if (horizontalMovement > 0) spriteRenderer.flipX = true;
    }

    private void PatrolAI() 
    {
        if (transform.position.x - startRoute >= endRoute)
        {
            horizontalMovement = -lightWalk;
        }
        if (transform.position.x - startRoute <= 0)
        {
            horizontalMovement = lightWalk;
        }
    }

    private void ShootAI() 
    {
        // Check if Hero is near.
        if (Mathf.Abs(player.transform.position.x - transform.position.x) <= detectionLength)
        {
            // Stop and Draw gun.
            horizontalMovement = 0;
            animator.SetBool("PointGun", true);
            // Look towards player.
            if (player.transform.position.x > transform.position.x) spriteRenderer.flipX = true;
            else spriteRenderer.flipX = false;

            if (timer >= timeToShoot)
            {
                timer = 0;
                // Shoot the Player.
                Vector2 flippedInstantiateVector = spriteRenderer.flipX ? bulletInstantiateVector * new Vector2(-1, 1) : bulletInstantiateVector;
                GameObject firedBullet = Instantiate(bullet, (Vector2)transform.position + flippedInstantiateVector, Quaternion.Euler(0, 0, 0));
                firedBullet.GetComponent<BulletController>().speed = spriteRenderer.flipX ? bulletSpeed : -bulletSpeed;
            }
        }
        else
        {
            // Hero is not close, keep patrolling.
            animator.SetBool("PointGun", false);
            if (horizontalMovement == 0) horizontalMovement = lightWalk;
            PatrolAI();
            timer = 0;
        }
    }
}
