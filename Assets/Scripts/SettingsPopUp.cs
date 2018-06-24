using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SettingsPopUp : MonoBehaviour {
    public static SettingsPopUp current;
    
    
    public Sprite musicOn, musicOff, soundOn, soundOff;

   
    public Image musicButton, soundButton;
    
    bool isMusicOn = true, isSoundOn = true;
    
    void Awake ()
    {
        gameObject.SetActive(false);
    }
    
	// Use this for initialization
	void Start () {
		current = this;
        isSoundOn = SoundManager.current.isSoundOn();
        isMusicOn = SoundManager.current.isMusicOn();
        soundButton.sprite = (isSoundOn) ? soundOn : soundOff;
        musicButton.sprite = (isMusicOn) ? musicOn : musicOff;
        
	}
	
     public void MusicClick()
    {
        isMusicOn = !isMusicOn;
        SoundManager.current.setMusicOn(isMusicOn);
        musicButton.sprite = (isMusicOn) ? musicOn : musicOff;
    }

    public void SoundClick()
    {
        isSoundOn = !isSoundOn;
        SoundManager.current.setSoundOn(isSoundOn);
        soundButton.sprite = (isSoundOn) ? soundOn : soundOff;
    }
    
    public void Close()
    {
        HeroRabbit rabbit=HeroRabbit.lastRabbit;
        if(rabbit!=null)
            rabbit.setActive();
        gameObject.SetActive(false);
    }

    public void Open()
    {
        HeroRabbit rabbit=HeroRabbit.lastRabbit;
        if(rabbit!=null)
            rabbit.setInActive();
        gameObject.SetActive(true);
    }
	
}
