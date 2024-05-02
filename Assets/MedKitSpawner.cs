
using UnityEngine;


public class MedKitSpawner : MonoBehaviour
{
    public GameObject medkit; // Reference to the medkit prefab
    public float spawnHeight = 1.0f; // Height at which the medkit will spawn above the spawner
    public float spawnInterval = 60.0f; // Interval between each medkit spawn
    private float timer; // Timer to track the spawning interval

    void Start()
    {
        timer = spawnInterval; // Start the timer at spawnInterval to spawn the first medkit immediately
    }

    void Update()
    {
        // Update the timer
        timer -= Time.deltaTime;

        // If the timer reaches zero or below, spawn a medkit and reset the timer
        if (timer <= 0)
        {
            SpawnMedkit();
            timer = spawnInterval;
        }
    }

    void SpawnMedkit()
    {
        // Instantiate a medkit prefab above the spawner's position
        Vector3 spawnPosition = transform.position + transform.up * spawnHeight;
        Instantiate(medkit, spawnPosition, Quaternion.identity);
    }
}