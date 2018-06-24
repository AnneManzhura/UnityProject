using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {
    
    public static LevelController current;
    Vector3 startingPosition;
    private string levelName;
    private LevelStat statistics;
    
    private int coins = 0;
    public int defaultFruits = 0;
    private int fruits = 0;
    private int crystals = 0;
    private int lives = 3;
    
    public Text coinsText;
    public Text fruitsText;
    public Text coinsTextPopUp;
    public Text fruitsTextPopUp;
    
    public Image[] heartsImages;
    public Image[] crystalImages;
    public Image[] crystalImagesWinPopUp;
    public Image[] crystalImagesLosePopUp;
    
    public Sprite liveUsed;
    public Sprite liveActive;
    public Sprite noCrystal;
    public Sprite heartSprite;
    public Sprite[] crystalSprites;
    
    void Awake() { 
        current = this;
        levelName = SceneManager.GetActiveScene().name;
        
        LoadStatistics();
    } 
    
     public void LoadStatistics()
    {
        this.statistics = LevelStat.Deserialize(levelName);
        if (this.statistics == null) this.statistics = new LevelStat();
    }
    
    void Update()
    {
        coinsText.text = coins.ToString("D4");
        fruitsText.text = string.Format("{0}/{1}", fruits, defaultFruits);
        coinsTextPopUp.text = "+" + coins.ToString();
        fruitsTextPopUp.text = string.Format("{0}/{1}", fruits, defaultFruits);
    }
    
    public void setStartPosition(Vector3 pos) { 
        this.startingPosition = pos;
    }
    
    public void onRabbitDeath(HeroRabbit rabbit) {
        //При смерті кролика повертаємо на початкову позицію
        if(lives>=0){
            lives--;
            heartsImages[lives].sprite = liveUsed;
            rabbit.playDeathSound();
            if (lives == 0)
            {
                StartCoroutine(whileDying(1, rabbit, false)); 
            }   
            else
            {
                StartCoroutine(whileDying(1, rabbit, true)); 
            }  
        }
    }
    
    IEnumerator whileDying(float sec, HeroRabbit rabbit, bool hasMoreLives)
    {
        Animator animator = rabbit.GetComponent<Animator>();
        animator.SetBool("dead",true);
        yield return new WaitForSeconds(sec);
        rabbit.transform.position = this.startingPosition; 
        animator.SetBool("dead", false);
        if(!hasMoreLives){
            rabbit.setInActive();
           LoseLevelPopUp.current.Open();
        }  
    }
    
    public void save()
    {
        if (statistics.levelPassed)
        {
            if (crystals == 3) statistics.hasCrystals = true;
            if (fruits == defaultFruits) statistics.hasAllFruits = true;
            PlayerPrefs.SetString(levelName, JsonUtility.ToJson(this.statistics));

            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins", 0) + coins);
            PlayerPrefs.Save();
        }
    }
    
    public void setLevelPassed(){
        statistics.levelPassed = true;
    }
    
    public void addCoins(int n) 
    {
        coins += n;
    }
    
    public void addCrystals(int n) 
    {
        crystals++;
        crystalImages[n].sprite = crystalSprites[n];
        crystalImagesWinPopUp[n].sprite = crystalSprites[n];
        crystalImagesLosePopUp[n].sprite = crystalSprites[n];
    }
    
    public void addFruits(int id)
    {
        fruits++;
        if (!fruitIsPickedUp(id))
            statistics.collectedFruits.Add(id);
    }
    
    public void addLives()
    {
        if(lives<3)
        {
            heartsImages[lives].sprite = liveActive;
            lives++;
            
        }
    }
    
    public void incrementFruitNumber() 
    {
        fruits++;
    }

    public bool fruitIsPickedUp(int id)
    {
        return statistics.collectedFruits.Contains(id);
    }
    
}
