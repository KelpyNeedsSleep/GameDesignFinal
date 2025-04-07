using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Sprite[] HeartSprites;

    public Image HeartUI;

    private PlayerController player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();


    }


    void Update()
    {
        if (player.curHealth < 0) { player.curHealth = 0; }
        {

            HeartUI.sprite = HeartSprites[player.curHealth];
        }
    }
}
