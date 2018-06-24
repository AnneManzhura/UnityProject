using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabbit: MonoBehaviour {
    public static HeroRabbit lastRabbit = null;
    
    private Rigidbody2D myBody = null;
    private SpriteRenderer sr = null;
    private Animator animator = null;
    private Transform heroParent = null;
    
    Vector3 sizeDefault;
    
    public float speed = 1f;
    public float MaxJumpTime = 2f; 
    public float JumpSpeed = 2f;
    float JumpTime = 0f;
    float invincibleTime = 4f;
         
    private bool isGrounded = false;
    private bool JumpActive = false;
    private bool isDead = false;
    private bool isInvincible = false;
    private bool isBig = false;
    private bool isActive = true;
    private bool groundSoundPlayed = true;
    
    public AudioClip runClip = null;
    public AudioClip dieClip = null;
    public AudioClip groundClip = null;
    private AudioSource runSource = null;
    private AudioSource dieSource = null;
    private AudioSource groundSource = null;
    
    void Awake() {
        lastRabbit = this;
    }
    
	void Start () {
		myBody = this.GetComponent<Rigidbody2D> ();
        myBody.freezeRotation = true;
        sr = GetComponent<SpriteRenderer>();
        animator=GetComponent<Animator> ();
        if (LevelController.current != null) 
            LevelController.current.setStartPosition(transform.position);
	    this.heroParent = this.transform.parent;
	    sizeDefault = transform.lossyScale;
        
        runSource = gameObject.AddComponent<AudioSource>();
        runSource.clip = runClip;
        runSource.loop = true;
        dieSource = gameObject.AddComponent<AudioSource>();
        dieSource.clip = dieClip;
        groundSource = gameObject.AddComponent<AudioSource>();
        groundSource.clip = groundClip;
	}
    
    void FixedUpdate () {
        if(isActive)
        {
            float value = Input.GetAxis ("Horizontal");
        
        if (Mathf.Abs (value) > 0 && !isDead) {
            Vector2 vel = myBody.velocity;
            vel.x = value * speed;
            myBody.velocity = vel;
            sr.flipX = (value > 0) ? false : true;
            animator.SetBool ("run", true);
            if (!runSource.isPlaying && SoundManager.current.isSoundOn()) runSource.Play();
        }
        else
        {
            runSource.Stop();
            animator.SetBool ("run", false);
        }
            if (!isGrounded) runSource.Stop();
            
            
          
        
        //Ground
        
        Vector3 from = transform.position + Vector3.up * 0.3f;
        Vector3 to = transform.position + Vector3.down * 0.1f;
        int layer_id = 1 << LayerMask.NameToLayer("Ground");
        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
        if (hit)
        {
            isGrounded = true;
            if (hit.transform != null && hit.transform.GetComponent<MovingPlatform>() != null)
            {
                SetNewParent(this.transform, hit.transform);
            }
        }
        else
        {
            isGrounded = false;
            SetNewParent(this.transform, this.heroParent);
        }
            
         if (isGrounded && !groundSoundPlayed && SoundManager.current.isSoundOn())
        {
            groundSoundPlayed = true;
            groundSource.Play();
        }
        
            
        
       
        if (Input.GetButtonDown("Jump") && isGrounded && !isDead)
            JumpActive = true;

        if (JumpActive)
            if (Input.GetButton("Jump")) {
                JumpTime += Time.deltaTime;
                if (JumpTime < MaxJumpTime)
                    myBody.velocity = new Vector2(myBody.velocity.x, JumpSpeed * (1.0f - JumpTime / MaxJumpTime));
            } else {
                groundSoundPlayed = false;
                this.JumpActive = false;
                this.JumpTime = 0;
        }
        
        
        
        //isInvincible
        
        if (isInvincible) {
            invincibleTime -= Time.deltaTime;
            GetComponent<SpriteRenderer>().material.color =
                new Color(1, 1 - invincibleTime % 1 / 2, 1 - invincibleTime % 1 / 2);
            if (invincibleTime < 0)
            {
                isInvincible = false;
                GetComponent<SpriteRenderer>().material.color =
                    new Color(1, 1, 1);
            }
        }
        
        
        
        if(this.isGrounded) {
            animator.SetBool ("jump", false);
        } else {
            animator.SetBool ("jump", true);
        }  
    }     
 }

    
    void SetSize(Vector3 size)
    {
        transform.localScale = Vector3.one;
        transform.localScale = new Vector3(size.x / transform.lossyScale.x, 
            size.y / transform.lossyScale.y, 
            size.z / transform.lossyScale.z);
    }
    
    public void makeRabbitBig()
    {
        isBig = true;
        //Current size
        SetSize(sizeDefault * 1.5f);
    }
    
    public void makeRabbitSmallOrDead()
    {
        if (isBig) {
            isBig = false;
            SetSize(sizeDefault);  
            invincibleTime = 4f;
            isInvincible = true;
        }
        else
        {
            Die();
        }
    }

    public void playDeathSound()
    {
        if(SoundManager.current.isSoundOn())dieSource.Play();
    }
    
    public void Die()
    {
        isDead = true;
        playDeathSound();
        LevelController.current.onRabbitDeath(this);
        isDead = false;
    }
    
    static void SetNewParent(Transform obj, Transform new_parent)
    {
        if (obj.transform.parent != new_parent)
        {
            //Засікаємо позицію у Глобальних координатах
            Vector3 pos = obj.transform.position;
            //Встановлюємо нового батька
            obj.transform.parent = new_parent;
                //Після зміни батька координати кролика зміняться
                //Оскільки вони тепер відносно іншого об’єкта
            
            //повертаємо кролика в ті самі глобальні координати
            obj.transform.position = pos;
        }
    }

    public void setInActive()
    {
        
        animator.SetBool ("jump", false);
        animator.SetBool ("run", false);
        runSource.Stop();
        
        isActive=false;
    }
    
     public void setActive()
    {
        
        isActive=true;
    }
    
    
    public bool isInvincibleNow()
    {
        return isInvincible;
    }
    
    
    
}
