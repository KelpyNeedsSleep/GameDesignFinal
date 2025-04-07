using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int maxHealth = 3;
    public int currentHealth;

    void start()
    {
        currentHealth = maxHealth;
    }
    
    public void damage(int damageToTake)
    {
        currentHealth =- damageToTake;
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log("enemy killed");
        }
        Debug.Log("damage taken");
    }
}
