using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack_Projetil : MonoBehaviour
{
    public GameObject projetilPrefab; // Prefab do projetil
    public Transform spawnPoint; // Ponto de spawn do projetil
    public float projetilVelocidade = 10f; // Velocidade pública do projetil
    public float tempoDeRecarga = 0.5f; // Tempo de delay entre os tiros (em segundos)

    private bool viradoDireita = true; // Controla a direção que o player está olhando
    private float tempoProximoTiro = 0f; // Controle interno do delay

    void Start()
    {
        // Inicializa o spawnPoint se não estiver definido
        if (spawnPoint == null)
        {
            spawnPoint = transform; // Usa a posição do player como ponto de spawn
        }
    }

    void Update()
    {
        // Atualiza a direção com base na escala do player (ou você pode usar outra lógica)
        if (Input.GetAxisRaw("Horizontal") > 0)
            viradoDireita = true;
        else if (Input.GetAxisRaw("Horizontal") < 0)
            viradoDireita = false;

        // Atira ao clicar com o botão direito e respeitando o tempo de recarga
        if (Input.GetMouseButtonDown(1) && Time.time >= tempoProximoTiro)
        {
            Atirar();
            tempoProximoTiro = Time.time + tempoDeRecarga; // Atualiza o tempo do próximo tiro
        }
    }

    void Atirar()
    {
        GameObject projetil = Instantiate(projetilPrefab, spawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = projetil.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Define a velocidade do projetil baseado na direção
            float direcao = viradoDireita ? 1f : -1f;
            rb.velocity = new Vector2(direcao * projetilVelocidade, 0f);
        }
    }
}
