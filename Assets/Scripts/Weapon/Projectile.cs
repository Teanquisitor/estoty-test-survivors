using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int damage;

    public void Initialize(int damage) => this.damage = damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<Enemy>(out var enemy))
            return;

        enemy.Health.ChangeHealth(-damage);
        Destroy(gameObject);
    }

}