using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 3; // Vida do inimigo
    public float damageImpulseForce = 5f; // Força do impulso ao tomar dano

    private Animator anim;
    private Rigidbody2D rb;

    bool canTakeDamage = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage, bool isFacingRight)
    {
        if (canTakeDamage)
        {
            health -= damage;
            anim.SetTrigger("hurt");

            // Aplica um impulso ao inimigo ao tomar dano
            float direction = isFacingRight ? -1 : 1; // Inverte a direção
            Vector2 damageImpulse = new Vector2(damageImpulseForce * direction, damageImpulseForce);
            rb.AddForce(damageImpulse, ForceMode2D.Impulse);

            if (health <= 0)
            {
                Die();
            }
            StartCoroutine(damageCooldown());
        }
    }

    IEnumerator damageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(0.5f);
        canTakeDamage = true;
    }

    void Die()
    {
        // Lógica para a morte do inimigo (destruir o objeto, tocar animação, etc.)
        anim.SetTrigger("die");
        //desabilitar components...
        GetComponent<EnemyAttack>().enabled = false;
        GetComponent<EnemyMove>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 3);
        canTakeDamage = false;
    }
}