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

    public bool canTakeDamage = true;
    private SpriteRenderer spriteRenderer;

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
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        if (canTakeDamage)
        {

            lives--;
            StartCoroutine("animationDamege");
            anim.SetTrigger("hurt");

            // Aplica um impulso ao jogador ao tomar dano
            rb.velocity = Vector2.zero;
            float direction = !GetComponent<Move>().isFacingRight ? -1 : 1; // Inverte a direção
            Vector2 damageImpulse = new Vector2(damageImpulseForce * direction, damageImpulseForce);
            rb.AddForce(damageImpulse, ForceMode2D.Impulse);

            UpdateLifeSlider();

            if (lives <= 0)
            {
                print("Game Over");
            }
            StartCoroutine("damageDelay");
            // Inicia a corrotina para fazer o sprite piscar
            StartCoroutine(BlinkSprite());
        }
    }

    IEnumerator damageDelay()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(0.5f);
        canTakeDamage = true;
    }

    IEnumerator BlinkSprite()
    {
        while (!canTakeDamage)
        {
            spriteRenderer.color = Color.gray; // Muda a cor para cinza
            yield return new WaitForSeconds(0.1f); // Espera 0.1 segundos
            spriteRenderer.color = Color.white; // Muda a cor para normal
            yield return new WaitForSeconds(0.1f); // Espera 0.1 segundos
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
        yield return new WaitForSeconds(0.3f);
        GetComponent<Move>().canMove = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), true);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), false);
        }
    }

}