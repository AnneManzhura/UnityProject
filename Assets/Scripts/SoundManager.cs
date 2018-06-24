using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    
    public static SoundManager current = new SoundManager();
    
	private bool is_sound_on = true;
    private bool is_music_on = true;

    public bool isSoundOn()
    {
        return is_sound_on;
    }

    public bool isMusicOn()
    {
        return is_music_on;
    }

    public void setSoundOn(bool val)
    {
        this.is_sound_on = val;
        PlayerPrefs.SetInt("sound", is_sound_on ? 1 : 0); 
        PlayerPrefs.Save();
    }

    public void setMusicOn(bool val)
    {
        this.is_music_on = val;
        PlayerPrefs.SetInt("music", is_music_on ? 1 : 0);
        PlayerPrefs.Save();
    }

    SoundManager()
    {
        is_sound_on = PlayerPrefs.GetInt("sound", 1) == 1;
        is_music_on = PlayerPrefs.GetInt("music", 1) == 1;
    }

    
}
