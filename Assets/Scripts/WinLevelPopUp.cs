using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinLevelPopUp : MonoBehaviour {

    
    public AudioClip winClip;
    AudioSource winSource;
    
static public WinLevelPopUp current = null;

    private void Awake()
    {
        current = this;
        winSource = gameObject.AddComponent<AudioSource>();
        winSource.clip = winClip;
        winSource.playOnAwake = false;
        gameObject.SetActive(false);
    }
    
    public void Repeat()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void Open()
    {
        gameObject.SetActive(true);
        if (SoundManager.current.isSoundOn()) winSource.Play();
        
    }

    public void Next()
    {
        SceneManager.LoadScene("MainMenu");
    }

   
}
