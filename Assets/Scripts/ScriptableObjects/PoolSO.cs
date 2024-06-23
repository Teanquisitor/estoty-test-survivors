using UnityEngine;

[CreateAssetMenu(fileName = "New Entity Pool", menuName = "SO/Entity Pool")]
public class PoolSO : ScriptableObject
{
    public string tag;
    public int initialPoolSize;
    public int maxPoolSize;

    public EntitySettings entitySettings;
    public LootTable experience;
    public LootTable[] lootTables;

}

[System.Serializable]
public struct EntitySettings
{
    public GameObject prefab;
    public int health;
    public int damage;
    public float speed;
    public float attackRange;
    public float damageInterval;
}

[System.Serializable]
public struct LootTable
{
    public GameObject prefab;
    public int amount;
}