using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject coinPickup;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Aumenta o número de moedas do jogador
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.IncreaseCoins();
            }

            // Destroi a moeda
            Destroy(gameObject);
            if (coinPickup != null)
            {
                GameObject coinAnim = Instantiate(coinPickup);
                Destroy(coinAnim, 1f);
            }
        }
    }
}