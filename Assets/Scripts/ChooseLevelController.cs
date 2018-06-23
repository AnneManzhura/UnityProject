using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseLevelController : MonoBehaviour {

    public static ChooseLevelController current;
    public Vector3 startingPosition;
	    
	void Awake() { 
        current = this;       
    } 
}
