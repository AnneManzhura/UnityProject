using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHere : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    //Стандартна функція, яка викличеться, 
    //коли поточний об’єкт зіштовхнеться із іншим
    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("++");
        //Намагаємося отримати компонент кролика
        HeroRabbit rabit = collider.GetComponent<HeroRabbit> ();
        
        //Впасти міг не тільки кролик
        if(rabit != null) {
            //Повідомляємо рівень, про смерть кролика 
            LevelController.current.onRabitDeath (rabit);
            
        }
        
    }
    
}

