using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage= 50;
     public float coolDown = 1;
    public float coolDownTimer;
    // Start is called before the first frame update
    void Start()
    {
    //call animator when we have animation 

    }

    // Update is called once per frame
    void Update()
    {   if (coolDownTimer >0)
        {
            coolDownTimer-= Time.deltaTime;
        }
        if (coolDownTimer <0)
        {
            coolDownTimer=0;
        }
        if (Input.GetKeyDown(KeyCode.Space) && coolDownTimer == 0)
        {
            Attack();
            coolDownTimer = coolDown;
        }
       
     
    }

    void Attack()
    {
        //play attack animation
        //animator.SetTrigger("Attack");
        attackPoint.gameObject.SetActive(true);
        
        //detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        //damage
        foreach(Collider2D enemy in hitEnemies)
        {
           Debug.Log ("We hit " + enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
        attackPoint.gameObject.SetActive(false);
    }

    void OnDrawGizmosSelected(){
        if (attackPoint== null)
        return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
