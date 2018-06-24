using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownOrc : Orc {

	//Prefab з якого будуть копії
	public GameObject prefabCarrot;
    
	public float radiusToRabbit = 3f;
  
    public float AttackInterval = 2f;
	private float lastCarrot = 0f;
	
	
	protected override void attackRabbit()
	{
		if (!rabbitDead)
		{
			animator.SetBool("walk", false);
			float value = DirectionToRabbit();
			sr.flipX = (value > 0) ? false : true;
			
            Vector3 rabbit_pos = HeroRabbit.lastRabbit.transform.position;
            Vector3 my_pos = transform.position;
            
			if (Time.time - lastCarrot > AttackInterval) {
                Debug.Log("Attack carrot");
				lastCarrot = Time.time;
                animator.SetTrigger("attack");
				launchCarrot(DirectionToRabbit());					
			}
			
		}
	}
    
    protected override bool RabbitInRadius() {
		Vector3 my_pos = this.transform.position;
		Vector3 rabbit_pos = HeroRabbit.lastRabbit.transform.position;
        return Mathf.Abs(rabbit_pos.x - my_pos.x) < 5.0f;
	}
    
    
	void launchCarrot(float direction) {
		//Створюємо копію Prefab
		GameObject obj = Instantiate(this.prefabCarrot); //Розміщуємо в просторі
		obj.transform.position = this.transform.position + new Vector3(0, 1, 0);
		//Запускаємо в рух
		Carrot carrot = obj.GetComponent<Carrot> ();
        if (SoundManager.current.isSoundOn()) attackSource.Play();
		carrot.Launch (direction);
	}
	
}
