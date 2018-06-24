using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class CoinsChooseLevel : MonoBehaviour {

    public Text numberOfCoins = null;
    
	void Start () {
        numberOfCoins.text = PlayerPrefs.GetInt("coins", 0).ToString("D4");
	}
}
