using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public int knocktime;
    public float power;
    public bool knockedback= false;
    public Transform attackPoint;
   

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody2D enemy = other.gameObject.GetComponent<Rigidbody2D>();

            if (enemy != null)
            {
                Debug.Log("In knockback");
                // enemy.isKinematic= false;
                Vector2 difference = enemy.transform.position - transform.position;
                difference = difference.normalized * power;
                enemy.AddForce(difference, ForceMode2D.Impulse);
                //enemy.isKinematic= true;
                knockedback = true;
                //StartCoroutine(KnockCo(enemy));
            }
        }
    }
    /*private IEnumerator KnockCo(Rigidbody2D enemy)
    {
        if (enemy!= null)
        {
            yield return new WaitForSeconds(knocktime);
            enemy.velocity= Vector2.zero;
            enemy.isKinematic= true;
        }
    }*/
}
