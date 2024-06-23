using UnityEngine;

[CreateAssetMenu(fileName = "New Spawner Settings", menuName = "SO/Spawner Settings")]
public class SpawnerSO : ScriptableObject
{
    public float initialSpawnInterval = 5f;
    public float minSpawnInterval = 0.25f;
    public float spawnRateIncreaseInterval = 30f;
    public float spawnRateIncreaseAmount = 0.1f;
    public float initialSpawnDelay = 2f;
}