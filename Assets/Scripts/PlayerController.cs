using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    //Components set in editor
    [SerializeField] private GameObject gameoverUI;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D thisRigidbody;
    [SerializeField] private GameObject[] weapons;
    public healthbar healthbar;
    
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private AudioClip switchWeaponSound;

    //Balance variables
    [SerializeField] private float speed;
    [SerializeField] private int maxHealth;
    [SerializeField] private float attackRange;
     
    //Process variables
    private Vector2 direction;
    private Vector2 facing;
    private float timer;

    private float flip;
    private float adjustx = 0;
    private float adjusty = 0;

    private int health;
    private bool facingRight = true;
    private bool attacking;
    [SerializeField] private WeaponCodes currentWeapon = WeaponCodes.SWORD;
    private string currentAnim;
    
    //Weapons
    public enum WeaponCodes {
        SWORD,
        AXE
    }
    
    //Animations
    private const string PLAYER_IDLE = "PlayerIdle";
    private const string PLAYER_LEFT = "PlayerWalkLeft";
    private const string PLAYER_RIGHT = "PlayerWalkRight";
    private const string PLAYER_DOWN = "PlayerWalkDown";
    private const string PLAYER_UP = "PlayerWalkUp";

    void ChangeAnimation(String newAnim) {
        //Stop from playing the same anim again
        if (currentAnim == newAnim ) { return; }
        //Play new and set current var
        animator.Play(newAnim);
        currentAnim = newAnim;
    }
    
    private void Start() {
        ResetHealth();
        ChangeAnimation(PLAYER_IDLE);
        healthbar.SetMaxHealth(health);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && !attacking) {
            attacking = true;
            Attack();
            
            
        }

        timer += Time.deltaTime;
        if (timer >= 10) {
            currentWeapon = (WeaponCodes)UnityEngine.Random.Range(0, 2);
            _audioManager.Play(switchWeaponSound);
            timer = 0;
        }
    }
    
    void Attack() {
        if (facing.x != 1 && facing.x != 0) {
            facing.y = 0;
            facing.Normalize();
            flip= 1;
            adjustx= 0;
            adjusty = 0;

        }
        int rotation = 0;

        if ((int)facing.x <= -1) {
            rotation = -0;
            flip=1;
             adjustx= 0;
            adjusty = 0;
            //good -0
        }
        else if((int)facing.y >= 1) {
            rotation =0;
            flip=1;
            adjustx= 0;
            adjusty = 0;
            //good 0
        }else if((int)facing.y <= -1) {
            rotation = 90;
            flip=-1;
            adjustx= 0;
            adjusty = 0;
            //good 90
        }
        //Instantiation of the current weapon, position based on the facing direction
        //var temp = new Vector3 (adjustx,adjusty,0);
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
        

        if (direction.magnitude > 0 && !attacking) {
            facing = direction;
            thisRigidbody.velocity = direction * (speed * Time.deltaTime);
            if (direction.x > 0) {
                ChangeAnimation(PLAYER_RIGHT);
            }else if (direction.x < 0) {
                ChangeAnimation(PLAYER_LEFT);
            }else if (direction.y > 0) {
                ChangeAnimation(PLAYER_UP);
            }else {
                ChangeAnimation(PLAYER_DOWN);
            }
        }
        else {
            ChangeAnimation(PLAYER_IDLE);
        }
        
       /* if (direction.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (direction.x < 0 && facingRight)
        {
            Flip();
        }*/
    }
    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x*= -1;
        gameObject.transform.localScale = currentScale;

        facingRight =! facingRight;
    }

    private void ResetHealth() {
        maxHealth=100;
        health = maxHealth;
    }

    public void TakeDamageP(int dmg) {
        health -= dmg;
        healthbar.SetHealth(health);
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
