using System.Collections.Generic;
using DependencyInjection;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Inject] private Player player;
    [Inject] private MainCamera mainCamera;
    [SerializeField] private List<PoolSO> pools;
    [SerializeField] private SpawnerSO spawnerSettings;
    [SerializeField] private float spawnRadius = 25f;

    private Dictionary<string, Queue<GameObject>> poolDictionary;
    private float timer;
    private float currentSpawnInterval;
    private float lastSpawnRateIncreaseTime;

    public Dictionary<string, Queue<GameObject>> PoolDictionary => poolDictionary;

    private void Start()
    {
        InitializePools();
        currentSpawnInterval = spawnerSettings.initialSpawnInterval;

        InvokeRepeating(nameof(SpawnEnemies), spawnerSettings.initialSpawnDelay, currentSpawnInterval);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        UpdateSpawnRate();
    }

    private void InitializePools()
    {
        poolDictionary = new();

        foreach (var pool in pools)
        {
            var objectPool = new Queue<GameObject>();

            for (var i = 0; i < pool.poolSize; i++)
            {
                var obj = CreatePoolObject(pool.entitySettings.prefab);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    private GameObject CreatePoolObject(GameObject prefab)
    {
        var obj = Instantiate(prefab, transform);
        obj.SetActive(false);
        return obj;
    }

    private void UpdateSpawnRate()
    {
        if (timer - lastSpawnRateIncreaseTime >= spawnerSettings.spawnRateIncreaseInterval)
        {
            currentSpawnInterval = Mathf.Max(currentSpawnInterval - spawnerSettings.spawnRateIncreaseAmount, spawnerSettings.minSpawnInterval);
            lastSpawnRateIncreaseTime = timer;

            CancelInvoke(nameof(SpawnEnemies));
            InvokeRepeating(nameof(SpawnEnemies), 0f, currentSpawnInterval);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position)
    {
        if (!poolDictionary.TryGetValue(tag, out var objectPool))
            return null;

        var objectToSpawn = objectPool.Dequeue();

        if (objectToSpawn.activeInHierarchy)
        {
            objectPool.Enqueue(objectToSpawn);
            return null;
        }

        SetupSpawnedObject(objectToSpawn, tag, position);
        objectPool.Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    private void SetupSpawnedObject(GameObject obj, string tag, Vector3 position)
    {
        var enemy = obj.transform.GetComponent<Enemy>();
        var pool = pools.Find(x => x.tag == tag);

        var enemySettings = pool.entitySettings;
        var loot = new LootTable[]{ pool.experience, pool.lootTables[Random.Range(0, pool.lootTables.Length)] };

        enemy.Initialize(player, pool.entitySettings, loot);
        obj.transform.SetPositionAndRotation(position, Quaternion.identity);
        obj.SetActive(true);
    }

    private void SpawnEnemies()
    {
        var spawnPosition = GetPositionOutsideView();
        var enemyTag = pools[Random.Range(0, pools.Count)].tag;
        SpawnFromPool(enemyTag, spawnPosition);
    }

    private Vector3 GetPositionOutsideView()
    {
        var cameraPosition = mainCamera.transform.position;
        var randomDirection = Random.insideUnitCircle;

        var normalizedDirection = new Vector3(randomDirection.x, randomDirection.y, 0f).normalized;
        cameraPosition.z = 0;

        return cameraPosition + normalizedDirection * spawnRadius;
    }

}