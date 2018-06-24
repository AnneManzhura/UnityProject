using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Collectable {
   
    private Vector3 direction;
    
  
    public float speed = 3f;
    
    void Start() {
        StartCoroutine (destroyLater ());
    }

    IEnumerator destroyLater() {
        yield return new WaitForSeconds (3f); 
        Destroy (this.gameObject);
    }
    
	public void Launch(float dir) {
        direction = new Vector3(dir, 0, 0);
		transform.localScale = new Vector3((dir > 0) ? -1 : 1, transform.localScale.y,1);
    }
    
      void Update()
    {
        transform.position += -direction * speed * Time.deltaTime;
    }
    
    
      void OnTriggerEnter2D(Collider2D collider) 
    {
        if (this.isActiveAndEnabled)
        {
            HeroRabbit rabbit = collider.GetComponent<HeroRabbit>();
            if (rabbit != null)
            {
                rabbit.Die();
                Destroy(this.gameObject);
            }
        }
    }
    
}
