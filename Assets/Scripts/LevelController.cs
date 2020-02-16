using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public AudioClip winMusic, loseMusic;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Won()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(winMusic);
    }

    public void Dead()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(loseMusic);
    }
}
