using System.Linq;
using UnityEngine;
using Zenject;

public class Weapon : MonoBehaviour
{
    [Inject] private AudioManager audioManager;
    private WeaponSettings settings;

    private int currentAmmo;
    private float lastShotTime;

    public void Initialize(WeaponSettings settings)
    {
        this.settings = settings;
        currentAmmo = settings.initialAmmo;
    }

    public void ChangeAmmo(int amount) => currentAmmo += amount;

    public void AimAndShoot(Vector3 position)
    {
        var colliders = Physics2D.OverlapCircleAll(position, settings.fireRange);
        var closestEnemy = colliders
            .Select(c => c.GetComponent<Enemy>()) // get rid of GetComponent
            .Where(e => e != null)
            .OrderBy(e => Vector2.Distance(position, e.transform.position))
            .FirstOrDefault();

        if (closestEnemy is null)
            return;

        var directionToEnemy = (closestEnemy.transform.position - position).normalized;
        var angle = Mathf.Atan2(directionToEnemy.y, directionToEnemy.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (currentAmmo > 0 && Time.time - lastShotTime >= 1f / settings.fireRate)
            Shoot(directionToEnemy);
    }

    private void Shoot(Vector2 direction)
    {
        var bullet = Instantiate(settings.projectilePrefab, transform.position, transform.rotation);
        bullet.GetComponent<Projectile>().Initialize(settings.damage);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * settings.projectileSpeed;

        var lifetime = settings.fireRange / settings.projectileSpeed;
        Destroy(bullet, lifetime);

        currentAmmo--;
        lastShotTime = Time.time;

        audioManager.Play(settings.shootSound);
    }

}