using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int maxHealth = 100;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

   private IEnumerator waitDelayDie()
    {

        yield return new WaitForSeconds(1);
        Die();

    }
    public void Die()
    {
        Debug.Log ("Enemy Died");
        Destroy(gameObject);
        //die animation
        //disable enemy
    }

    public void TakeDamage (int damage)
    {
        currentHealth-= damage;
        //play hurt animation
        if (currentHealth<= 0)
        {
           StartCoroutine(waitDelayDie());
            
        }

    }
    

}
