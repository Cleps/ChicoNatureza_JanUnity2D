using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Animator anim;
    public bool isAttacking;

    public int attackDamage = 1; // Dano do ataque
    public float attackRange = 0.5f; // Alcance do ataque
    public LayerMask enemyLayers; // Camada dos inimigos

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAttack();
        direction = !GetComponent<Move>().isFacingRight ? Vector2.left : Vector2.right;
    }

    void PlayerAttack()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking) // 0 is for left mouse button
        {
            anim.SetTrigger("attack");
            StartCoroutine("animationAttack");
        }
    }

    Vector2 direction;
    IEnumerator animationAttack()
    {
        GetComponent<Move>().canMove = false;
        isAttacking = true;
        // Detectar inimigos no alcance do ataque usando Raycast
        RaycastHit2D[] hitEnemies = Physics2D.RaycastAll(transform.position, direction, attackRange, enemyLayers);

        // Causar dano aos inimigos detectados
        foreach (RaycastHit2D hit in hitEnemies)
        {
            if (hit.collider != null)
            {
                hit.collider.GetComponent<Enemy>().TakeDamage(attackDamage, GetComponent<Move>().isFacingRight);
                //print("Atacou");
            }
        }

        yield return new WaitForSeconds(0.5f);
        GetComponent<Move>().canMove = true;
        isAttacking = false;
    }

    // Desenhar o alcance do ataque no editor da Unity
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 direction = GetComponent<Move>().isFacingRight ? Vector2.right : Vector2.left;
        Gizmos.DrawRay(transform.position, direction * attackRange);
    }
}