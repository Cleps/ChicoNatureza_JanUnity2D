using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject axePrefab;
    public Transform player; // Referência ao jogador
    private EnemyMove enemyMove;

    private GameObject axe;
    private bool isCollidingWithPlayer = false;
    public float axeThrowInterval = 2f; // Intervalo de tempo entre lançamentos de machado
    public float raycastDistance = 5f; // Distância do Raycast

    void Start()
    {
        enemyMove = GetComponent<EnemyMove>();
    }

    void Update()
    {
        Color rayColor = CheckPlayerCollision();

        // Desenhar o Raycast no editor da Unity
        Vector2 direction = enemyMove.movingRight ? Vector2.right : Vector2.left;
        Debug.DrawRay(transform.position, direction * raycastDistance, rayColor);

        if (isCollidingWithPlayer)
        {
            StartCoroutine(InstatiateAxe());
        }
    }

    IEnumerator stopPlayer()
    {
        enemyMove.canMove = false;
        yield return new WaitForSeconds(5f);
        enemyMove.canMove = true;
    }

    IEnumerator InstatiateAxe()
    {
        while (isCollidingWithPlayer)
        {
            StartCoroutine(stopPlayer());
            yield return new WaitForSeconds(axeThrowInterval);
            ThrowAxe();
        }
    }

    void ThrowAxe()
    {
        if (axe == null)
        {
            axe = Instantiate(axePrefab, transform.position, Quaternion.identity);
            axe.GetComponent<Rigidbody2D>().velocity = new Vector2(enemyMove.movingRight ? 5 : -5, 0);
            Destroy(axe, 6f);
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


}