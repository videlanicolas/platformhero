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
        yield return new WaitForSeconds(1);
        Image panel = canvas.GetComponentInChildren<Image>();
        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            yield return new WaitForSeconds(0.01f);
            panel.color = Color.Lerp(Color.clear, Color.black, i);
        }
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Level1");
    }


}
