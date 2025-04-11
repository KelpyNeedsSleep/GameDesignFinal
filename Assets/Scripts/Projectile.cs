using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [Range(0f,10f)]
    [SerializeField] private float speed;
    [Range(0f,10f)]
    [SerializeField] private float lifeTime;
    [Range(0f,10f)]
    [SerializeField] private float rangeDmg;
    [SerializeField] private LayerMask whatDestroysProjectile;
    
    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if((whatDestroysProjectile.value & (1 << collision.gameObject.layer)) > 0)
        {
            IDamageable iDamageable = collision.gameObject.GetComponent<IDamageable>();
            if(iDamageable != null)
            {
                iDamageable.Damage(rangeDmg);
            }
            
            IBreakable iBreakable = collision.gameObject.GetComponent<IBreakable>();
            if(iBreakable != null)
            {
                iBreakable.Break();
            }

            DestroyProjectile();
        }
    }

    void DestroyProjectile() 
    {
        Destroy(gameObject);
    }
}
