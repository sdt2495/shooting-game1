using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public Transform player;

    public float spawnInterval = 3f;

    public float spawnDistance = 20f;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        InvokeRepeating(
            nameof(SpawnEnemy),
            1f,
            spawnInterval
        );
    }

    void SpawnEnemy()
    {
        if (player == null) return;

        Vector3 spawnPos = GetSpawnPosition();

        Instantiate(
            enemyPrefab,
            spawnPos,
            Quaternion.identity
        );
    }

    Vector3 GetSpawnPosition()
    {
        Vector3 camForward = mainCamera.transform.forward;

        camForward.y = 0;

        camForward.Normalize();

        Vector3 randomOffset =
            Quaternion.Euler(0, Random.Range(-40f, 40f), 0)
            * camForward;

        Vector3 spawnPos =
            player.position + randomOffset * spawnDistance;

        return spawnPos;
    }
}