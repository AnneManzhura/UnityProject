using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider)
    {
        if (this.isActiveAndEnabled)
        {
            HeroRabbit rabbit = collider.GetComponent<HeroRabbit>();
            if (rabbit != null)
            {
                LevelController.current.setLevelPassed();
                LevelController.current.save();
                rabbit.setInActive();
                WinLevelPopUp.current.Open();
            }
        }
    }
}
