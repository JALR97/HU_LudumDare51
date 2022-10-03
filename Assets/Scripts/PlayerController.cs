using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //Components set in editor
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D thisRigidbody;
    
    //Balance variables
    [SerializeField] private float speed;
    [SerializeField] private float maxHealth;
     private bool facingRight = true;
    //Process variables
    private Vector2 direction;
    private float health;

    private void Start() {
        ResetHealth();
    }

    private void Update() {
    }

    private void FixedUpdate()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        direction.Normalize();
        

        if (direction.magnitude > 0) {
            thisRigidbody.velocity = direction * (speed * Time.deltaTime);
        animator.SetTrigger("Moving");
        }
        if (direction.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (direction.x < 0 && facingRight)
        {
            Flip();
        }
    }
    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x*= -1;
        gameObject.transform.localScale = currentScale;

        facingRight =! facingRight;
    }

    private void ResetHealth() {
        maxHealth=100f;
        health = maxHealth;
    }

    public void TakeDamageP(int dmg) {
        health -= dmg;
        Debug.Log("Ouchie");
        
        if (health <= 0) {
            Die();
        }
    }

    private void Die() {
        //Game over behavior
        Debug.Log("Gameover");
        Time.timeScale = 0;
    }
}
