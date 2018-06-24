using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoseLevelPopUp : MonoBehaviour {

    static public LoseLevelPopUp current = null;

    public AudioClip loseClip;
    AudioSource loseSource;
    
    private void Awake()
    {
        current = this;
        loseSource = gameObject.AddComponent<AudioSource>();
        loseSource.clip = loseClip;
        loseSource.playOnAwake = false;
        gameObject.SetActive(false);
	}
	
    public void Open()
    {
        gameObject.SetActive(true);
        if (SoundManager.current.isSoundOn()) loseSource.Play();

    }
    public void Repeat()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void Next()
    {
        SceneManager.LoadScene("MainMenu");
    }

    
}
