using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public AudioClip buttonSelect;

    GameObject canvas;
    AudioSource audioSource;

    private void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartMenu());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        StartCoroutine(StartLevel("Level1"));
    }

    IEnumerator StartLevel(string levelName)
    {
        audioSource.PlayOneShot(buttonSelect);
        CanvasGroup panel = canvas.GetComponentInChildren<CanvasGroup>();
        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            yield return new WaitForSeconds(0.01f);
            float v = Mathf.Lerp(1, 0, i);
            panel.alpha = v;
            audioSource.volume = v;
        }
        SceneManager.LoadScene(levelName);
    }

    IEnumerator StartMenu()
    {
        CanvasGroup panel = canvas.GetComponentInChildren<CanvasGroup>();
        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            yield return new WaitForSeconds(0.01f);
            float v = Mathf.Lerp(0, 1, i);
            panel.alpha = v;
            audioSource.volume = v;
        }

    }
}
