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

    void Start()
    {
        enemyMove = GetComponent<EnemyMove>();
    }

    void Update()
    {

    }

    IEnumerator stopPlayer()
    {
        enemyMove.canMove = false;
        yield return new WaitForSeconds(5f);
        enemyMove.canMove = true;
    }

    IEnumerator InstatiateAxe()
    {
        StartCoroutine(stopPlayer());
        yield return new WaitForSeconds(2f);
        ThrowAxe();
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && IsFacingPlayer())
        {
            StartCoroutine(InstatiateAxe());
        }
    }

    bool IsFacingPlayer()
    {
        if (player == null) return false;

        Vector2 directionToPlayer = player.position - transform.position;
        if (enemyMove.movingRight && directionToPlayer.x > 0)
        {
            return true;
        }
        else if (!enemyMove.movingRight && directionToPlayer.x < 0)
        {
            return true;
        }
        return false;
    }
}