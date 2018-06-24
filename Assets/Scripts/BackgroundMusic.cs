using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {

    
    public AudioClip music = null;
    private AudioSource musicSource = null;

	void Start () {
        
        musicSource = gameObject.AddComponent<AudioSource>(); 
        musicSource.clip = music;
        musicSource.loop = true;
        musicSource.Play();
	}

    private void Update()
    {
        if (!musicSource.isPlaying && SoundManager.current.isMusicOn()) musicSource.Play();
        if (!SoundManager.current.isMusicOn()) musicSource.Stop();
    }
}
