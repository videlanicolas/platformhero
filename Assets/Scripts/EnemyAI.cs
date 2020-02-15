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
    public AIType AItype = AIType.None;
    public float endRoute;

    Rigidbody2D rigidBody;
    Animator animator;
    float horizontalMovement, startRoute;
    AImethod method;

    const float lightWalk = 0.1f,
                fastWalk = 0.5f,
                run = 1f;

    private void Awake()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        animator = gameObject.GetComponent<Animator>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();

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
        animator.SetFloat("Velocity", Mathf.Abs(horizontalMovement));
        // Call the method that executes the AI.
        method();
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(horizontalMovement * speed, rigidBody.velocity.y);
        if (horizontalMovement < 0)
        {
            if (gameObject.GetComponent<SpriteRenderer>().flipX) gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (horizontalMovement > 0)
        {
            if (!gameObject.GetComponent<SpriteRenderer>().flipX) gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void PatrolAI() 
    {
        if ((transform.position.x - startRoute >= endRoute) || (transform.position.x - startRoute <= 0))
        {
            horizontalMovement = -horizontalMovement;
        }
    }

    private void ShootAI() 
    {

    }
}
