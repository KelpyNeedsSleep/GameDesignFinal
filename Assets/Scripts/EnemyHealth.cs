using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [Range(0f,100f)]
    [SerializeField] private float maxHealth;
    [Range(0f,100f)]
    [SerializeField] private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }
    
    public void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log("enemy killed");
        }
    }
}
