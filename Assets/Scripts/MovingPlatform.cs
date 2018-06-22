using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
	public Vector3 MoveBy;
	Vector3 pointA;
	Vector3 pointB;
	bool going_to_a = false;
	public float speed = 3f;

	public float waitingTime = 2f;
	float time_to_wait;

	void Start()
	{
		this.pointA = this.transform.position;
		this.pointB = this.pointA + MoveBy;
		time_to_wait = waitingTime;
	}

	void Update()
	{
		time_to_wait -= Time.deltaTime;
		if (time_to_wait <= 0)
		{
			Vector3 my_pos = this.transform.position;
			Vector3 target = (going_to_a) ? this.pointA : this.pointB;
			Vector3 destination = target - my_pos;
			destination.z = 0;
			destination.Normalize();
			this.transform.position += destination * speed * Time.deltaTime;
			if (isArrived(my_pos, target))
			{
				going_to_a = !going_to_a;
				time_to_wait = waitingTime;
			}
		}
	}
//чи доїхала платформа
	bool isArrived(Vector3 pos, Vector3 target)
	{
		pos.z = 0;
		target.z = 0;
		return Vector3.Distance(pos, target) < 0.02f;
	}

	
	
}
