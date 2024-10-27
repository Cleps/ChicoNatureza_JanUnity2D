using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovel : MonoBehaviour
{
    public float speed = 2f; // Velocidade de movimento
    public float moveTime = 2f; // Tempo de movimento em cada direção

    private float moveTimer;
    private bool movingLeft = true;

    // Start is called before the first frame update
    void Start()
    {
        moveTimer = moveTime;
    }

    // Update is called once per frame
    void Update()
    {
        // Atualizar o contador de tempo
        moveTimer -= Time.deltaTime;

        // Verificar se o tempo de movimento acabou
        if (moveTimer <= 0f)
        {
            // Alternar a direção do movimento
            movingLeft = !movingLeft;
            moveTimer = moveTime;
        }

        // Mover a plataforma
        if (movingLeft)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }
}