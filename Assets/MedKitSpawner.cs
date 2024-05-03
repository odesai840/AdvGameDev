
using UnityEngine;


public class MedKitSpawner : MonoBehaviour
{
    public GameObject medkit; // Reference to the medkit prefab
    public float spawnHeight = 1.0f; // Height at which the medkit will spawn above the spawner
    public float spawnInterval = 60.0f; // Interval between each medkit spawn
    private float timer; // Timer to track the spawning interval

    void Start()
    {
        timer = 0; 
    }

    void Update()
    {
        // Update the timer if there's no medkit above the spawner
        if (!IsMedkitAboveSpawner())
        {
            timer -= Time.deltaTime;
        }
        else {
            timer = spawnInterval;
        }

        if (timer <= 0)
        {
            SpawnMedkit();
            timer = spawnInterval;
        }
    }

    void SpawnMedkit()
    {
        Vector3 spawnPosition = transform.position + transform.up * spawnHeight;
        Instantiate(medkit, spawnPosition, Quaternion.Euler(-90, 0, 0));
    }

    bool IsMedkitAboveSpawner()
    {
        Vector3 spawnPosition = transform.position + transform.up * spawnHeight;

        // Cast a sphere upwards to check if there's a medkit above the spawner
        Collider[] colliders = Physics.OverlapSphere(spawnPosition, 1.0f); // Adjust radius as needed

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("MedKit"))
            {
                return true;
            }
        }
        return false;
    }
}