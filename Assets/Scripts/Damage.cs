using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player")) {

            player.Damage(1);

            //this will knock back the player if colliding with the "fire" object. 
            player.knockbackCount = player.knockbackLength;

            //if the x.position of the fire is to your right, the player will get knocked back to the left.
            if (other.transform.position.x < transform.position.x)
                player.knockFromRight = true;
            else
                player.knockFromRight = false;
        }
    }
    }
