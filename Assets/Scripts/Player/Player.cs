using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public int totalLives = 10;
    public int lives = 3;
    public int coins = 0;

    public Vector3 InitialPosition;
    public GameObject GameOverObject;
    public Transform UIparentObject;

    public TMP_Text coinsText;
    public Slider lifeSlider; // Slider para representar a vida do jogador

    private Animator anim;
    private Rigidbody2D rb;

    public float damageImpulseForce = 5f; // Força do impulso ao tomar dano

    public GameObject impactEffect;
    public bool canTakeDamage = true;
    private SpriteRenderer spriteRenderer;

    private AudioSource audioSource;
    public AudioClip damageSound;
    public AudioClip pickupFruitSound;
    public AudioClip gameOverSound;

    internal void IncreaseCoins()
    {
        coins++;
        if (pickupFruitSound != null)
            audioSource.PlayOneShot(pickupFruitSound);
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        UpdateLifeSlider();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
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
            coinsText.text = "Coletados: " + coins.ToString();
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
            Instantiate(impactEffect, transform.position, Quaternion.identity);
            UpdateLifeSlider();

            if (lives <= 0)
            {
                GetComponent<Move>().enabled = false;
                GetComponent<Attack>().enabled = false;

                // Desativar dois CapsuleCollider2D
                CapsuleCollider2D[] colliders = GetComponents<CapsuleCollider2D>();
                foreach (CapsuleCollider2D collider in colliders)
                {
                    collider.enabled = false;
                }
                StartCoroutine("delayInGameOver");
                if (damageSound != null)
                    audioSource.PlayOneShot(damageSound);
            }
            else
            {
                if (damageSound != null)
                    audioSource.PlayOneShot(damageSound);
            }
            StartCoroutine("damageDelay");
            // Inicia a corrotina para fazer o sprite piscar
            StartCoroutine(BlinkSprite());
        }
    }

    IEnumerator delayInGameOver()
    {
        yield return new WaitForSeconds(2f);
        makeGameOver();
    }


    IEnumerator damageDelay()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(1f);
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

    public IEnumerator BlinkSpriteTimer(float blinkTime)
    {
        float elapsedTime = 0f;
        while (elapsedTime < blinkTime)
        {
            spriteRenderer.color = Color.gray; // Muda a cor para cinza
            yield return new WaitForSeconds(0.1f); // Espera 0.1 segundos
            spriteRenderer.color = Color.white; // Muda a cor para normal
            yield return new WaitForSeconds(0.1f); // Espera 0.1 segundos
            elapsedTime += 0.2f; // Incrementa o tempo decorrido
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

    public void makeGameOver()
    {
        print("Game Over");
        if (gameOverSound != null)
            audioSource.PlayOneShot(gameOverSound);
        Instantiate(GameOverObject, UIparentObject); // Instancia o objeto como filho do objeto especificado
        GetComponent<SpriteRenderer>().sortingOrder = -4;
        StartCoroutine(gameOverAnimation());
    }

    IEnumerator gameOverAnimation()
    {
        yield return new WaitForSeconds(2f);
        lives = totalLives;
        GetComponent<SpriteRenderer>().sortingOrder = 1;
        GetComponent<Move>().enabled = true;
        GetComponent<Attack>().enabled = true;
        // Desativar dois CapsuleCollider2D
        CapsuleCollider2D[] colliders = GetComponents<CapsuleCollider2D>();
        foreach (CapsuleCollider2D collider in colliders)
        {
            collider.enabled = true;
        }
        UpdateLifeSlider();
        transform.position = InitialPosition;
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