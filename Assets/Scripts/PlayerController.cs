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
    }

    private void ResetHealth() {
        health = maxHealth;
    }

    public void TakeDamage(int dmg) {
        health -= dmg;
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
