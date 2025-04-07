using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    private BoxCollider2D boxCollider;
    public float fallDelay;
    
    
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();


    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        myRigidBody.isKinematic = false;
        boxCollider.isTrigger = true;
        yield return 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
