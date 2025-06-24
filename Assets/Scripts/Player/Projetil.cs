using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projetil : MonoBehaviour
{
    public int damage = 10; // Dano causado pelo projetil

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Verifica a direção do projetil em relação ao inimigo
            bool isLookingRight = (transform.position.x < collision.transform.position.x);

            // Chama a função TakeDamage no inimigo
            collision.GetComponent<Enemy>().TakeDamage(damage, !isLookingRight);

            // Destroi o projetil após causar dano
            Destroy(gameObject);
        }
    }
}
