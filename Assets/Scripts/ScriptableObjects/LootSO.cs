using UnityEngine;

[CreateAssetMenu(fileName = "New Loot Settings", menuName = "SO/Loot Settings")]
public class LootSO : ScriptableObject
{
    public int value = 5;
    public float attractionRadius = 5f;
    public float attractionSpeed = 5f;
    public float collectRadius = 0.5f;
}