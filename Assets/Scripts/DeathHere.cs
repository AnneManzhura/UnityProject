using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHere : MonoBehaviour {

    public bool isLevel;
    
    //Стандартна функція, яка викличеться, 
    //коли поточний об’єкт зіштовхнеться із іншим
    void OnTriggerEnter2D(Collider2D collider)
    {
        //Намагаємося отримати компонент кролика
        HeroRabbit rabit = collider.GetComponent<HeroRabbit> ();
        
        //Впасти міг не тільки кролик
        if(rabit != null) {
            //Повідомляємо рівень, про смерть кролика 
            if(isLevel)
            {
               LevelController.current.onRabbitDeath (rabit); 
            }
            else
            {     
                rabit.transform.position = ChooseLevelController.current.startingPosition; 
            }
        }
        
    }
    
}

