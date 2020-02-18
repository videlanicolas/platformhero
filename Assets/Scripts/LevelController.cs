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

    GameObject player, enemies;
    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        canvas = GameObject.FindGameObjectWithTag("Canvas");

        player = GameObject.FindGameObjectWithTag("Player");
        enemies = GameObject.FindGameObjectWithTag("Enemies");
    }
    // Start is called before the first frame update
    void Start()
    {
        player.SetActive(false);
        enemies.SetActive(false);
        StartCoroutine(StartLevel());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Won()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(winMusic);
        StartCoroutine(LoadMenu());
    }

    public void Dead()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(loseMusic);
        StartCoroutine(ReloadScene());
    }

    IEnumerator LoadMenu()
    {
        Image panel = canvas.GetComponentInChildren<Image>();
        yield return new WaitForSeconds(4);
        player.SetActive(false);
        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            yield return new WaitForSeconds(0.01f);
            panel.color = Color.Lerp(Color.clear, Color.black, i);
        }
        SceneManager.LoadScene("Menu");
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

    IEnumerator StartLevel()
    {
        yield return new WaitForSeconds(1);
        Image panel = canvas.GetComponentInChildren<Image>();
        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            yield return new WaitForSeconds(0.01f);
            panel.color = Color.Lerp(Color.black, Color.clear, i);
        }
        yield return new WaitForSeconds(1);
        player.SetActive(true);
        enemies.SetActive(true);
        audioSource.Play();
    }


}
