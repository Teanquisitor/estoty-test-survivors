using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    private static List<Enemy> activeEnemies = new List<Enemy>();
    private float separationDistance = 1f;
    private float lootDropRadius = 1f;

    private Player target;
    private Health health;
    private LootTable[] dropTables;
    private EntitySettings entity;

    private float lastDamageTime;

    public static UnityAction OnEnemyDied;

    public Health Health => health;

    private void Awake() => health = GetComponent<Health>();
    private void OnEnable()
    {
        health.OnDied += HandleDeath;

        activeEnemies.Add(this);
    }

    private void OnDisable()
    {
        activeEnemies.Remove(this);

        health.OnDied -= HandleDeath;
    }

    private void Update()
    {
        if (target == null)
            return;

        var direction = (target.transform.position - transform.position).normalized;
        var distanceToPlayer = Vector2.Distance(target.transform.position, transform.position);

        var moveDirection = (Vector2)direction + CalculateSeparationForce();
        moveDirection = moveDirection.normalized;

        if (distanceToPlayer > entity.attackRange)
            transform.position = Vector2.MoveTowards(transform.position, transform.position + (Vector3)moveDirection, entity.speed * Time.deltaTime);

        else
        {
            if (Time.time <= lastDamageTime + entity.damageInterval)
                return;

            DealDamage();
            lastDamageTime = Time.time;
        }
    }

    public void Initialize(Player target, EntitySettings entity, LootTable[] dropTables)
    {
        this.target = target;
        this.entity = entity;
        this.dropTables = dropTables;

        health.Initialize(entity.health);
        health.ResetHealth();
    }

    public static Enemy GetEnemyFromCollider(Collider2D collider)
    {
        foreach (var enemy in activeEnemies)
        {
            if (enemy.gameObject == collider.gameObject)
                return enemy;
        }
        return null;
    }

    private Vector2 CalculateSeparationForce()
    {
        var separationForce = Vector2.zero;

        foreach (var enemy in activeEnemies)
        {
            if (enemy == this)
                continue;
                
            var distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < separationDistance)
            {
                var directionToEnemy = (transform.position - enemy.transform.position).normalized;
                separationForce += (Vector2)directionToEnemy / distance;
            }
        }

        return separationForce;
    }

    private void DealDamage() => target.Health.ChangeHealth(-entity.damage);

    private void HandleDeath()
    {
        OnEnemyDied?.Invoke();

        foreach (var loot in dropTables)
        {
            for (int i = 0; i < loot.amount; i++)
            {
                var dropPosition = transform.position + Random.insideUnitSphere * lootDropRadius;
                var lootPrefab = Instantiate(loot.prefab, dropPosition, Quaternion.identity);

                lootPrefab.GetComponent<Loot>().Initialize(target);
            }
        }

        gameObject.SetActive(false);
    }

}