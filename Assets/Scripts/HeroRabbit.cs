﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabbit : MonoBehaviour {

    public float speed = 1;
    
    private Rigidbody2D myBody = null;
    private SpriteRenderer sr = null;
    
	// Use this for initialization
	void Start () {
		myBody = this.GetComponent<Rigidbody2D> ();
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    void FixedUpdate () { 
    float value = Input.GetAxis ("Horizontal");
        
    if (Mathf.Abs (value) > 0) {
        Vector2 vel = myBody.velocity;
        vel.x = value * speed;
        myBody.velocity = vel;
        }
    if(value < 0) {
        sr.flipX = true;
    } else if(value > 0) {
        sr.flipX = false;
    }    
        
    }
}
