using UnityEngine;

[CreateAssetMenu(fileName = "New Player Settings", menuName = "SO/Player Settings")]
public class PlayerSO : ScriptableObject
{
    public int maxHealth;
    public float speed;
    public int levelUpExperience;

    public WeaponSettings weaponSettings;
}

[System.Serializable]
public struct WeaponSettings
{
    public GameObject projectilePrefab;
    public string shootSound;
    public int initialAmmo;
    public int damage;
    public float projectileSpeed;
    public float fireRate;
    public float fireRange;
}