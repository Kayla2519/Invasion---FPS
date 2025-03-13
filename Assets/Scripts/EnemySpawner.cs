using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private bool _spawnEnemies = true;
    [SerializeField] private float spawnInterval = 2f;

    // Begin Spawning Routine

    IEnumerator SpawnRoutine()
    {
        Vector3 spawnPos = new Vector3(-1f, 1f, -3f);

        while (_spawnEnemies)
        {
            // Wait a set time - 2 seconds
            yield return new WaitForSeconds(spawnInterval);

            // Spawn Enemy
            spawnPos.x = Random.Range(-9f, 9f);
            spawnPos.y = 5f;
            Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
            
            // Loop back to wait
        }
    }

    void Start()
    {
        // Begin Spawning Routine
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

// Source: https://thestreetdev.medium.com/spawning-enemies-with-coroutines-in-unity-718710a25155
