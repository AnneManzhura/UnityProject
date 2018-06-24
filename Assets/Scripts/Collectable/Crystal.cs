using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Collectable {

    public enum Color {RED, BLUE, GREEN};
     
    public Color color;
    
	protected override void OnRabbitHit (HeroRabbit rabit) {
		LevelController.current.addCrystals((int)color);
		this.CollectedHide ();
	}
}
