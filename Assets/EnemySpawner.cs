using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private float initialSpawnCooldown = 6f;
    [SerializeField] private float minSpawnCooldown = 1f;
    [SerializeField] private float cooldownDecreaseRate = 5f;
    [SerializeField] private float cooldownDecreaseInterval = 60f;

    private float spawnCooldown;
    private bool isSpawning = false;

    private void Start()
    {
        spawnCooldown = initialSpawnCooldown;
        StartCoroutine(DecreaseCooldownRoutine());
    }

    private void Update()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnRoutine());
        }
    }

    private IEnumerator SpawnRoutine()
    {
        Spawn();
        yield return new WaitForSeconds(spawnCooldown);
        isSpawning = false;
    }

    private void Spawn()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        Instantiate(characterPrefab, spawnPosition, Quaternion.identity, transform);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float xOffset = Random.Range(-5f, 5f);
        Vector3 spawnPosition = transform.position + new Vector3(xOffset, 0f, 0f);
        return spawnPosition;
    }

    private IEnumerator DecreaseCooldownRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(cooldownDecreaseInterval);
            DecreaseCooldown();
        }
    }

    private void DecreaseCooldown()
    {
        if (spawnCooldown > minSpawnCooldown)
        {
            spawnCooldown -= cooldownDecreaseRate;
            if (spawnCooldown < minSpawnCooldown)
                spawnCooldown = minSpawnCooldown;
        }
    }
}
