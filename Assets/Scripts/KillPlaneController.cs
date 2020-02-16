using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlaneController : MonoBehaviour
{
    GameObject levelController;

    private void Awake()
    {
        levelController = GameObject.FindGameObjectWithTag("LevelController");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
        if (collision.gameObject.CompareTag("Player"))
        {
            levelController.GetComponent<LevelController>().Dead();
        }
    }
}
