using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string nextSceneName;
    AudioSource fxSound;
    public AudioClip backMusic;
    // Start is called before the first frame update
    void Start()
    {
        fxSound = GetComponent<AudioSource>();
        fxSound.Play();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        else if ((Input.GetButtonDown("Submit")) || Input.GetMouseButtonDown(0))
            SceneManager.LoadScene(nextSceneName);
        
    }
}
