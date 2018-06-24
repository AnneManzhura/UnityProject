using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Door : MonoBehaviour {
    
    public SpriteRenderer crystal;
    public SpriteRenderer fruit;
    public SpriteRenderer check;
    public SpriteRenderer locked;
    
    public int level;
    
    private bool isOpen = true;
    private string levelName="";
    
        private void Awake()
    {
            levelName ="Level"+level;
            
            LevelStat stats = LevelStat.Deserialize(levelName);
            if (stats == null) stats = new LevelStat();
            
            crystal.enabled = stats.hasCrystals;
            fruit.enabled = stats.hasAllFruits;
            
            if(level>1){
                string prevLevelName="Level"+(level-1);
                
                if(LevelStat.Deserialize(prevLevelName)!=null)
                {
                    isOpen = LevelStat.Deserialize(prevLevelName).levelPassed;  
                }
                else isOpen=false;
            
            }
            
            locked.enabled = !isOpen;
            check.enabled = stats.levelPassed;
    }
    
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (this.isActiveAndEnabled && isOpen)
        {
            HeroRabbit rabbit = collider.GetComponent<HeroRabbit>();
            if (rabbit != null)
            {
                SceneManager.LoadScene(levelName);
            }
        }
    }
}
