using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public AudioClip winMusic, loseMusic;

    AudioSource audioSource;
    GameObject canvas;
    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        canvas = GameObject.FindGameObjectWithTag("Canvas");
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
        StartCoroutine(ReloadScene());
    }

    IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(3);
        // Fade to black.
        for (float i = 0; i < 255; i++)
        {
            yield return new WaitForSeconds(1f);
            canvas.GetComponentInChildren<Image>().color = new Color(0, 0, 0, i);
        }
        SceneManager.LoadScene("Level1");
    }


}
