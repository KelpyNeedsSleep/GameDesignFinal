using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float timeBtwAttack;
    [Range(0f,5f)]
    [SerializeField] private float startTimeBtwAttack;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform attackPos;
    [SerializeField] private GameObject player;
    [Range(0f,4f)]
    [SerializeField] private float attackAngle;
    [Range(0f,4f)]
    [SerializeField] private float attackRangeX;
    [Range(0f,4f)]
    [SerializeField] private float attackRangeY;
    [SerializeField] private LayerMask whatIsEnemies;
    [Range(0f,10f)]
    [SerializeField] private float meleeDmg;
    private Animator myAnim;
    [SerializeField] private bool meleeAtk;
    [SerializeField] private bool rangedAtk;
    
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        myAnim.SetBool("Melee", meleeAtk);
        if(timeBtwAttack <= 0)
        //can attack
        {
            if(Input.GetButtonDown("Ranged"))
            {
                //attack
                // rangedAtk = true;
                Instantiate(projectile, attackPos.position, Quaternion.Euler(0,0, 90 * -player.transform.localScale.x));
                Debug.Log("Ranged");
                timeBtwAttack = startTimeBtwAttack;
            }
            
            if(Input.GetButtonDown("Melee"))
            {
                //attack
                meleeAtk = true;
                Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), attackAngle, whatIsEnemies);
                for(int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyHealth>().Damage(meleeDmg);
                }
                timeBtwAttack = startTimeBtwAttack;
            }
            
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
            if(timeBtwAttack >= 0){
                meleeAtk = false;
                //rangedAtk = false;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(attackPos.position, attackRange);
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX, attackRangeY, 1));
    }
}