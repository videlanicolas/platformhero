using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerController : MonoBehaviour
{
    GameObject player, mainCamera, levelController;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
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
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            player.GetComponent<HeroController>().Won();
            mainCamera.GetComponent<CameraFollower>().Won();
            levelController.GetComponent<LevelController>().Won();
        }
    }
}
