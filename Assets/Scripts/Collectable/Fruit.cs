using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : Collectable {

    public int id = 0;
    private bool isCollected=false;
    
    void Start()
    {
        
        if (LevelController.current.fruitIsPickedUp(id)) {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
            LevelController.current.incrementFruitNumber();
            isCollected=true;
        }
            
    }
    
	protected override void OnRabbitHit (HeroRabbit rabit)
	{
        if(!isCollected){
            LevelController.current.addFruits(id);
		      this.CollectedHide ();
        }
	}
}
