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

    // Start is called before the first frame update
    void Start()
    {
    //call animator when we have animation 

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
                Attack();
        }
     
    }

    void Attack()
    {
       
        //play attack animation
        animator.SetTrigger("Attack");

        //detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        //damage
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log ("We hit " + enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }

    }

    void OnDrawGizmosSelected(){
        if (attackPoint== null)
        return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
