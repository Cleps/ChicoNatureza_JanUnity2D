using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arara : MonoBehaviour
{
    public float speed = 5f; // Velocidade de movimento para a esquerda
    public float zigzagAmplitude = 1f; // Amplitude do movimento de zigue-zague
    public float zigzagFrequency = 1f; // Frequência do movimento de zigue-zague

    private float initialY;

    // Start is called before the first frame update
    void Start()
    {
        initialY = transform.position.y; // Armazena a posição inicial no eixo Y
    }

    // Update is called once per frame
    void Update()
    {
        // Movimento para a esquerda
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Movimento de zigue-zague
        float newY = initialY + Mathf.Sin(Time.time * zigzagFrequency) * zigzagAmplitude;
        transform.position = new Vector2(transform.position.x, newY);
    }
}