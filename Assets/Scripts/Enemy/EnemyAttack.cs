using System;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject axePrefab;
    private Transform player;
    private EnemyMove enemyMove;
    private Animator animator;

    private GameObject axe;
    public float axeVelocity = 4f;
    public float axeThrowInterval = 2f;
    public float raycastDistance = 5f;

    private float axeThrowTimer = 0f;
    public float preThrowDelay = 0.5f;
    private float preThrowTimer = 0f;
    private bool isPreThrowing = false;
    private bool isCollidingWithPlayer = false;

    public bool useRaycastOffset = false;
    public float raycastOffsetX = 0f;
    public float raycastOffsetY = 0f;

    void Start()
    {
        enemyMove = GetComponent<EnemyMove>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Color rayColor = CheckPlayerCollision();

        Vector2 direction = enemyMove.movingRight ? Vector2.right : Vector2.left;
        Vector2 rayOrigin = transform.position;
        if (useRaycastOffset)
            rayOrigin += new Vector2(raycastOffsetX, raycastOffsetY);

        Debug.DrawRay(rayOrigin, direction * raycastDistance, rayColor);

        // Se player estiver no alcance, inimigo para
        enemyMove.canMove = !isCollidingWithPlayer;

        // Conta o tempo entre os arremessos
        if (axeThrowTimer > 0f)
            axeThrowTimer -= Time.deltaTime;

        // Começa o pre throw
        if (isCollidingWithPlayer && axeThrowTimer <= 0f && !isPreThrowing)
        {
            preThrowTimer = preThrowDelay;
            isPreThrowing = true;
        }

        // Conta pre throw e lança machado
        if (isPreThrowing)
        {
            preThrowTimer -= Time.deltaTime;
            if (preThrowTimer <= 0f)
            {
                ThrowAxe();
                axeThrowTimer = axeThrowInterval;
                isPreThrowing = false;
            }
        }
    }

    void ThrowAxe()
    {
        if (GetComponent<Enemy>().canTakeDamage)
        {
            animator.SetTrigger("Attack");
            axe = Instantiate(axePrefab, transform.position, Quaternion.identity);
            axe.GetComponent<Rigidbody2D>().velocity = new Vector2(
                enemyMove.movingRight ? axeVelocity : -axeVelocity, 0);
            Destroy(axe, 6f);
        }
    }

    Color CheckPlayerCollision()
    {
        if (player == null) return Color.red;

        Vector2 direction = enemyMove.movingRight ? Vector2.right : Vector2.left;
        Vector2 rayOrigin = transform.position;

        if (useRaycastOffset)
            rayOrigin += new Vector2(raycastOffsetX, raycastOffsetY);

        RaycastHit2D[] hits = Physics2D.RaycastAll(rayOrigin, direction, raycastDistance);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                isCollidingWithPlayer = true;
                return Color.blue;
            }
        }

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
