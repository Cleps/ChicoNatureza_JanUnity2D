using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public int lives = 3;
    public int coins = 0;

    public TMP_Text coinsText;
    public Slider lifeSlider; // Slider para representar a vida do jogador

    private Animator anim;
    private Rigidbody2D rb;

    public float damageImpulseForce = 5f; // Força do impulso ao tomar dano

    internal void IncreaseCoins()
    {
        coins++;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        UpdateLifeSlider();
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

    public void takeDamege()
    {
        lives--;
        StartCoroutine("animationDamege");
        anim.SetTrigger("hurt");

        // Aplica um impulso ao jogador ao tomar dano
        float direction = !GetComponent<Move>().isFacingRight ? -1 : 1; // Inverte a direção
        Vector2 damageImpulse = new Vector2(damageImpulseForce * direction, damageImpulseForce);
        rb.AddForce(damageImpulse, ForceMode2D.Impulse);

        UpdateLifeSlider();

        if (lives <= 0)
        {
            print("Game Over");
        }
    }

    void UpdateLifeSlider()
    {
        if (lifeSlider != null)
        {
            lifeSlider.value = lives;
        }
    }

    IEnumerator animationDamege()
    {
        GetComponent<Move>().canMove = false;
        print("Stopping Player");
        yield return new WaitForSeconds(0.5f);
        GetComponent<Move>().canMove = true;
    }
}