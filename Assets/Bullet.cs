using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f; // Adjust as needed
    private bool hasHit = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasHit)
        {
            // Check if the collided object is the player
            if (other.CompareTag("Player"))
            {

                UnityEngine.Debug.Log("Hit player");
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                }

                // Set hasHit to true to avoid multiple collisions
                hasHit = true;

                // Destroy the bullet GameObject after hitting the player
                Destroy(gameObject);
            }
            else if (!other.CompareTag("Enemy"))
            {
                // If the collided object is not an enemy or player, do something (optional)

                // Destroy the bullet GameObject regardless of what it collided with
                Destroy(gameObject);
            }
        }
    }
}