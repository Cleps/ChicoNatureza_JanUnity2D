using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float moveSpeed = 2f; // Velocidade de movimento
    public float waitTime = 2f; // Tempo de espera
    public float moveTime = 3f;
    public bool movingRight = true; // Direção do movimento
    private Animator animator; // Componente Animator
    private float waitTimer; // Timer para controlar o tempo de espera
    private float moveTimer; // Timer para controlar o tempo de movimento
    private bool isWaiting = true;

    public bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        waitTimer = waitTime;
        moveTimer = moveTime;
    }

    // Update is called once per frame
    void Update()
    {
        FlipSprite();
        if (canMove)
        {
            Movement();
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

    }

    void Movement()
    {
        // Alterna a direção do movimento quando o tempo de movimento termina
        // if (!isWaiting && moveTimer <= 0)
        // {

        // }

        if (isWaiting)
        {
            if (waitTimer <= 0)
            {
                // Alterna para o estado de movimento
                isWaiting = false;
                movingRight = !movingRight;
                waitTimer = waitTime;
                moveTimer = moveTime;
            }
            else
            {
                waitTimer -= Time.deltaTime;
            }

            // Inimigo está parado
            animator.SetBool("isMoving", false);
        }
        else
        {
            if (moveTimer <= 0)
            {
                // Alterna para o estado de espera
                isWaiting = true;
                moveTimer = moveTime;
            }
            else
            {
                moveTimer -= Time.deltaTime;
            }

            // Inimigo está se movendo
            animator.SetBool("isMoving", true);
            float moveDirection = movingRight ? 1 : -1;
            transform.Translate(Vector2.right * moveDirection * moveSpeed * Time.deltaTime);
        }


    }


    void FlipSprite()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (!movingRight)
        {
            spriteRenderer.flipX = true;
            //isFacingRight = false;
        }
        else
        {
            spriteRenderer.flipX = false;
            //isFacingRight = true;
        }
    }
}