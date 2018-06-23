using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Door : MonoBehaviour {
    
    //SpriteRenderer crystal;
   // SpriteRenderer fruit;
   // SpriteRenderer check;
   // SpriteRenderer lock;
    
    private bool isOpen = true;
    
    
        private void Awake()
    {
            /*
        if (stats.hasCrystals)
        {
            crystal.sprite = allCrystals;
            crystal.GetComponent<Transform>().localScale = new Vector3(.7f, .7f, 1f);
        }
        if (stats.hasAllFruits)
        {
            fruit.sprite = allFruits;
            fruit.GetComponent<Transform>().localScale = new Vector3(.8f, .8f, 1f);
        }
        
            
        if (LevelStats.Load(prevLevel) != null)
            isOpen = LevelStats.Load(prevLevel).levelPassed;
          */
            
        //lock.enabled = !isOpen;
        //check.enabled = stats.levelPassed;
    }
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (this.isActiveAndEnabled && isOpen)
        {
            HeroRabbit rabbit = collider.GetComponent<HeroRabbit>();
            if (rabbit != null)
            {
                SceneManager.LoadScene("Level1");
            }
        }
    }
}
