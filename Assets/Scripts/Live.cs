using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Live : Collectable {

    
    void Start()
    {
        
    }
    
	protected override void OnRabbitHit (HeroRabbit rabit)
	{
        
        LevelController.current.addLives();
        this.CollectedHide ();
        
	}
}
