using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{


    private PlayerController player;
    public int coinCount;
    public Text coinText;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = "Coins:" + coinCount;
    }

    public void AddCoins(int coinsToAdd)
    {

        coinCount = coinCount + coinsToAdd;
        coinText.text = "Coins:  " + coinCount;

    }
}
