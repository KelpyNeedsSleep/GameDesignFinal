using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    [SerializeField] private float startTimeBtwAttack;
    public GameObject projectile;
    [SerializeField] private Transform attackPos;
    [Range(0f,4f)]
    [SerializeField] private float attackAngle;
    [Range(0f,4f)]
    [SerializeField] private float attackRangeX;
    [Range(0f,4f)]
    [SerializeField] private float attackRangeY;
    [SerializeField] private LayerMask whatIsEnemies;
    [SerializeField] private int meleeAtkDmg;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBtwAttack <= 0)
        //can attack
        {
            if(Input.GetButtonDown("Ranged"))
            {
                //attack
                Instantiate(projectile, attackPos.position, Quaternion.identity);
                Debug.Log("Ranged");
                timeBtwAttack = startTimeBtwAttack;
            }
            
            if(Input.GetButtonDown("Melee"))
            {
                //attack
                Debug.Log("Melee");
                Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), attackAngle, whatIsEnemies);
                for(int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyHealth>().damage(meleeAtkDmg);
                }
                timeBtwAttack = startTimeBtwAttack;
            }
            
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(attackPos.position, attackRange);
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX, attackRangeY, 1));
    }
}
