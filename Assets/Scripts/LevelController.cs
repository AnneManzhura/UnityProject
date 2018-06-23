using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelController : MonoBehaviour {
public static LevelController current;
    Vector3 startingPosition;
    
    public int coins = 0;
    public int fruits = 0;
    public int crystals = 0;
    
    
    void Awake() { 
        current = this;
    } 
    
    public void setStartPosition(Vector3 pos) { 
        this.startingPosition = pos;
    }
    
    public void onRabbitDeath(HeroRabbit rabbit) {
        //При смерті кролика повертаємо на початкову позицію 
        StartCoroutine(whileDying(1, rabbit));
        
    }

    IEnumerator whileDying(float sec, HeroRabbit rabbit)
    {
        Animator animator = rabbit.GetComponent<Animator>();
        animator.SetBool("dead",true);
        yield return new WaitForSeconds(sec);
        animator.SetBool("dead", false);  
        rabbit.transform.position = this.startingPosition;
    }
    
    public void addCoins(int n) 
    {
        coins += n;
    }
    public void addCrystals(int n) 
    {
        crystals += n;
    }
    public void addFruits(int n) 
    {
        fruits += n;
    }
}
