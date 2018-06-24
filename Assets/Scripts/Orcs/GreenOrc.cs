using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenOrc : Orc {
	
	public float runSpeed = 3f;
    
	protected override void attackRabbit()
	{
		if (!rabbitDead)
		{
			animator.SetBool("run", true);
			transform.position -= new Vector3(DirectionToRabbit(), .0f, .0f) * runSpeed * Time.deltaTime;
		}
	}
}
