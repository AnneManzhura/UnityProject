using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MonoBehaviour {
	public enum Mode { GoToA, GoToB, Attack, Dead }
	
    
    public AudioClip attackClip = null;
    protected AudioSource attackSource = null;
    
   
    public AudioClip dieClip = null;
    protected AudioSource dieSource = null;
    
	public float walkSpeed = 1f;
	public float walkDistance=4f;
    
	private float waitForRabbitToDieTime=0f;
	
	protected SpriteRenderer sr = null;
	protected Animator animator = null;
	
	protected Mode mode;
	public Vector3 pointA, pointB;

	public static bool rabbitDead=false;
	
	public void Start ()
	{
		
		pointB = this.transform.position;
		pointA = pointB;
		pointA.x += walkDistance;

		sr = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		GetComponent<Rigidbody2D>().freezeRotation = true;	
        
         attackSource = gameObject.AddComponent<AudioSource>();
        attackSource.clip = attackClip;
        dieSource = gameObject.AddComponent<AudioSource>();
        dieSource.clip = dieClip;
	}

	void FixedUpdate()
	{
		if (mode != Mode.Dead)
		{
			if (!rabbitDead)
			{
				if (RabbitInRadius()) {
					mode = Mode.Attack;
					attackRabbit();
				
				} else {
					if (mode == Mode.GoToA) {
						if (IsArrived(pointA)) {
							mode = Mode.GoToB;
						}
					} else if (mode == Mode.GoToB) {
						if (IsArrived(pointB)) {
							mode = Mode.GoToA;
						}
					} else {
						mode = Mode.GoToA;
					}
					walk();
				}
			}
			else if (waitForRabbitToDieTime > 0) {
				waitForRabbitToDieTime -= Time.deltaTime;
			} else {
				rabbitDead = false;
				this.transform.position = pointB;
			}	
		} 
	}

	protected virtual bool RabbitInRadius()
	{
        if(HeroRabbit.lastRabbit!=null){
            Vector3 my_pos = this.transform.position;
		Vector3 rabbit_pos = HeroRabbit.lastRabbit.transform.position;
        
        return (System.Math.Abs(rabbit_pos.x-my_pos.x) < walkDistance && System.Math.Abs(rabbit_pos.y-my_pos.y) < 0.5f);
        }
		return false;
	}
	
	void walk()
	{
		animator.SetBool("walk", true);
		animator.SetBool("run", false);
		float value = this.GetDirection();
		transform.position += new Vector3(value, .0f, .0f) * walkSpeed * Time.deltaTime;
	}

	private bool IsArrived(Vector3 target) {	
		Vector3 my_pos = this.transform.position;
		return Mathf.Abs(my_pos.x - target.x) < 0.03f; 
	}

	protected float GetDirection()
	{
		Vector3 my_pos = this.transform.position;
		if (mode == Mode.GoToA) {
			if (my_pos.x > pointA.x) {
				sr.flipX = false;
				return -1;
			}
			sr.flipX = true;
			return 1;
		}
		if (mode == Mode.GoToB) {
			if (my_pos.x > pointB.x) {
				sr.flipX = false;
				return -1;
			}
			sr.flipX = true;
			return 1;
		}
		return 0;
		
	}
	
	void OnCollisionEnter2D(Collision2D collision) {
		if (this.isActiveAndEnabled) {
			HeroRabbit rabbit = collision.gameObject.GetComponent<HeroRabbit>();
			if (rabbit != null) {
				this.OnRabbitHit(rabbit);
				
			}
		}
	}
	
	void OnRabbitHit(HeroRabbit rabbit) {
		Vector3 v = rabbit.transform.position - transform.position;
		float angle = Mathf.Atan2(v.y, v.x) / Mathf.PI * 180;
		if (angle > 60f && angle < 150f)
		{
			Die();
		} else
		{
            if (SoundManager.current.isSoundOn()) attackSource.Play();
			animator.SetTrigger("attack");
			rabbitDead = true;
			rabbit.Die();
			waitForRabbitToDieTime = 1f;
		}
	}
	
	protected float DirectionToRabbit() {
		Vector3 rabbit_pos = HeroRabbit.lastRabbit.transform.position;
		Vector3 my_pos = this.transform.position;
		if (mode == Mode.Attack) {
			if (my_pos.x < rabbit_pos.x) {
				sr.flipX = true;
				return -1;
			}

			sr.flipX = false;
			return 1;
		}

		return 0;
	}
	
    
    private IEnumerator DieCoroutine()
    {
        mode = Mode.Dead;
        animator.SetTrigger("dead");
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

    public void Die()
    {
        if (SoundManager.current.isSoundOn()) dieSource.Play();
        StartCoroutine(DieCoroutine());
    }
	
	protected virtual void attackRabbit() {}
}
