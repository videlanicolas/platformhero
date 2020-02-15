using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIType { 
    Patrol,
    Shoot,
}

public class EnemyAI : MonoBehaviour
{
    [Range(0.1f, 10f)]
    public float speed;
    public GameObject player;

    Rigidbody2D rigidBody;
    Animator animator;
    float horizontalMovement;

    private void Awake()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        animator = gameObject.GetComponent<Animator>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        horizontalMovement = -0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Velocity", Mathf.Abs(horizontalMovement));
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
}
