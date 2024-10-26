using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public int lives = 3;
    public int coins = 0;

    public TMP_Text coinsText;

    internal void IncreaseCoins()
    {
        coins++;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Updatecoins();

    }

    void Updatecoins()
    {
        if (coinsText != null)
        {
            coinsText.text = "Coletaveis: " + coins.ToString();
        }
    }
}
