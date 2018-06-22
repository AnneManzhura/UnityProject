using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushRoom : Collectable {

	protected override void OnRabbitHit(HeroRabbit rabbit)
	{
		rabbit.makeRabbitBig();
		this.CollectedHide();
	}
}
