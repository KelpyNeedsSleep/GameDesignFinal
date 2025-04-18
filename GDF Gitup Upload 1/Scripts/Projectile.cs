﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed;
    public float lifeTime;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;

    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null) {
            if (hitInfo.collider.CompareTag("Enemy")) {
                
                hitInfo.collider.GetComponent<EnemyHealth>().damage(damage);
            }
            DestroyProjectile();
        }


        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void DestroyProjectile() {
        Destroy(gameObject);
    }
}
