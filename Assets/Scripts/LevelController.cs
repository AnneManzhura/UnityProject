using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {
    
    public static LevelController current;
    Vector3 startingPosition;
    private string levelName;
    
    private int coins = 0;
    public int defaultFruits = 0;
    private int fruits = 0;
    private int crystals = 0;
    private int lives = 3;
    
    public Text coinsText;
    public Text fruitsText;
    
    public Image[] heartsImages;
    public Image[] crystalImages;
    
    public Sprite liveUsed;
    public Sprite noCrystal;
    public Sprite heartSprite;
    public Sprite[] crystalSprites;
    
    void Awake() { 
        current = this;
        levelName = SceneManager.GetActiveScene().name;
    } 
    
    void Update()
    {
        coinsText.text = coins.ToString("D4");
        fruitsText.text = string.Format("{0}/{1}", fruits, defaultFruits);
    }
    
    public void setStartPosition(Vector3 pos) { 
        this.startingPosition = pos;
    }
    
    public void onRabbitDeath(HeroRabbit rabbit) {
        //При смерті кролика повертаємо на початкову позицію 
        lives--;
        if (lives == 0)
            StartCoroutine(whileDying(1, rabbit, false));
        else{
            StartCoroutine(whileDying(1, rabbit, true));
            heartsImages[lives].sprite = liveUsed;
        } 
    }

    IEnumerator whileDying(float sec, HeroRabbit rabbit, bool hasMoreLives)
    {
        Animator animator = rabbit.GetComponent<Animator>();
        animator.SetBool("dead",true);
        yield return new WaitForSeconds(sec);
        if(hasMoreLives){
           animator.SetBool("dead", false);
            rabbit.transform.position = this.startingPosition; 
        }
        else
        {
           SceneManager.LoadScene ("ChooseLevel"); 
        }
    }
    
    public void addCoins(int n) 
    {
        coins += n;
    }
    public void addCrystals(int n) 
    {
        crystals++;
        crystalImages[n].sprite = crystalSprites[n];
    }
    public void addFruits(int n) 
    {
        fruits += n;
    }
}
