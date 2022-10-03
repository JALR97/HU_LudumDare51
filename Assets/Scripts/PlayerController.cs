using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //Components set in editor
    [SerializeField] private GameObject gameoverUI;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D thisRigidbody;
    [SerializeField] private GameObject[] weapons;
    
    //Balance variables
    [SerializeField] private float speed;
    [SerializeField] private float maxHealth;
    [SerializeField] private float attackRange;
     
    //Process variables
    private Vector2 direction;
    private Vector2 facing;
    private float health;
    private bool facingRight = true;
    private bool attacking;
    [SerializeField] private WeaponCodes currentWeapon = WeaponCodes.SWORD;
    
    //Weapons
    public enum WeaponCodes {
        SWORD,
        AXE
    }

    private void Start() {
        ResetHealth();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && !attacking) {
            attacking = true;
            Attack();
        }
    }
    
    void Attack() {
        if (facing.x < 1 && facing.x > 0) {
            facing.y = 0;
            facing.Normalize();
        }
        int rotation = 0;
        if ((int)facing.x == -1) {
            rotation = 180;
        }
        else if((int)facing.y == 1) {
            rotation = 90;
        }else if((int)facing.y == -1) {
            rotation = -90;
        }
        //Instantiation of the current weapon, position based on the facing direction
        Instantiate(weapons[(int)currentWeapon], transform.position + (Vector3)facing * attackRange, Quaternion.Euler(0, 0, rotation));
    }

    public void AttackDone() {
        attacking = false;
    }
    
    private void FixedUpdate()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        direction.Normalize();
        

        if (direction.magnitude > 0) {
            facing = direction;
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
        if (health <= 0) {
            Die();
        }
    }

    private void Die() {
        gameoverUI.SetActive(true);
        Debug.Log("Gameover");
        Time.timeScale = 0;
    }
}
