using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject axePrefab;
    public Transform player; // Referência ao jogador
    private EnemyMove enemyMove;
    private Animator animator;

    private GameObject axe;
    public float axeVelocity = 4f; // Velocidade do machado
    private bool isCollidingWithPlayer = false;
    public float axeThrowInterval = 2f; // Intervalo de tempo entre lançamentos de machado
    public float raycastDistance = 5f; // Distância do Raycast

    private bool canThrowAxe = true; // Variável para controlar se o machado pode ser lançado
    private float axeThrowTimer = 0f; // Contador para o intervalo de lançamento do machado
    private float stopPlayerTimer = 0f; // Contador para parar o movimento do inimigo
    public float preThrowDelay = 2f; // Tempo de atraso antes de lançar o machado
    private float preThrowTimer = 0f; // Contador para o atraso antes de lançar o machado
    private bool isPreThrowing = false; // Variável de controle para indicar que o atraso está em andamento

    void Start()
    {
        enemyMove = GetComponent<EnemyMove>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Color rayColor = CheckPlayerCollision();

        // Desenhar o Raycast no editor da Unity
        Vector2 direction = enemyMove.movingRight ? Vector2.right : Vector2.left;
        Debug.DrawRay(transform.position, direction * raycastDistance, rayColor);

        if (isCollidingWithPlayer && canThrowAxe && !isPreThrowing)
        {
            // Iniciar o atraso antes de lançar o machado
            preThrowTimer = preThrowDelay;
            isPreThrowing = true;
            enemyMove.canMove = false; // Pare o movimento do inimigo
        }

        // Atualizar o contador do atraso antes de lançar o machado
        if (isPreThrowing)
        {
            preThrowTimer -= Time.deltaTime;
            if (preThrowTimer <= 0f)
            {
                // Lançar o machado após o atraso
                ThrowAxe();
                axeThrowTimer = axeThrowInterval;
                isPreThrowing = false;
            }
        }

        // Atualizar o contador do intervalo de lançamento do machado
        if (axeThrowTimer > 0f)
        {
            axeThrowTimer -= Time.deltaTime;
        }

        // Atualizar o contador para parar o movimento do inimigo
        if (stopPlayerTimer > 0f)
        {
            stopPlayerTimer -= Time.deltaTime;
            if (stopPlayerTimer <= 0f)
            {
                enemyMove.canMove = true;
            }
        }
    }

    void ThrowAxe()
    {
        if (canThrowAxe)
        {
            canThrowAxe = false;
            animator.SetTrigger("Attack");
            axe = Instantiate(axePrefab, transform.position, Quaternion.identity);
            axe.GetComponent<Rigidbody2D>().velocity = new Vector2(enemyMove.movingRight ? axeVelocity : -axeVelocity, 0);
            Destroy(axe, 6f);
            canThrowAxe = true; // Permita que o machado seja lançado novamente após o intervalo
            stopPlayerTimer = axeThrowInterval; // Defina o tempo para parar o movimento do inimigo
        }
    }

    Color CheckPlayerCollision()
    {
        if (player == null) return Color.red;

        Vector2 direction = enemyMove.movingRight ? Vector2.right : Vector2.left;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, raycastDistance);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                //Debug.Log("Player detected");
                isCollidingWithPlayer = true;
                return Color.blue;
            }
        }
        //Debug.Log("Player not detected");
        isCollidingWithPlayer = false;
        return Color.red;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().takeDamege();
        }
    }
}