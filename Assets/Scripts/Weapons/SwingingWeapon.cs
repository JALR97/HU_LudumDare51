using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class SwingingWeapon : MonoBehaviour
{
    //Components set in editor
    [SerializeField] private Transform sprite;
    
    
    //Balance variables
    [SerializeField] private float lifetime;
    [SerializeField] private int damage;
    [SerializeField] private float knockback;
    [SerializeField] AnimationCurve rotationCurve;
    [SerializeField] private float coolDown;
    
    //Process variables
    private float timer = 0;
    private PlayerController playerC;
    private bool izquierda = false;

    private void Awake() {
        //We deactivate the parent sprite since the players are not to see it.
        GetComponent<SpriteRenderer>().enabled = false;
        playerC = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        
        if ((transform.position - playerC.transform.position).x > 0) {
            Debug.Log("Flipping");
            izquierda = true;
        }

        transform.parent = playerC.gameObject.transform;
    }

    private void Update() {
        var rotationSpeed = rotationCurve.Evaluate(timer / lifetime);
        

        if (!izquierda) {
            rotationSpeed *= -1;
        }
        
        sprite.Rotate(Vector3.back * (rotationSpeed * Time.deltaTime));
        
        timer += Time.deltaTime;

       
        if (timer >= lifetime) {
            StartCoroutine(End());
        }
    }

    private IEnumerator End() {
        //Small cooldown to keep the player from attacking again and
        //the weapon from vanishing right away
        yield return new WaitForSeconds(coolDown);
        playerC.AttackDone(); //Tell the attack script we can attack again
        Destroy(gameObject); //Bye weapon
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            //Cause damage to the enemy
            other.GetComponent<Enemy>().TakeDamage(damage);
            //And push them back
            Knockback(other.GetComponent<Rigidbody2D>());
        }
    }

    private void Knockback(Rigidbody2D enemy) {
        Vector2 difference = enemy.transform.position - transform.position;
        difference = difference.normalized * knockback;
        enemy.AddForce(difference, ForceMode2D.Impulse);
    }
}
