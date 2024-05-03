
using UnityEngine;


public class Medkit : MonoBehaviour
{
    public float healAmount = 30f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                if (!(playerHealth.GetHealth() >= 100))
                {
                    playerHealth.RestoreHealth(healAmount);
                    Destroy(gameObject); // Destroy the medkit after healing the player
                }

            }
        }
    }
}