using System;
using DependencyInjection;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IDependencyProvider
{
    [Provide] private Player ProvidePlayer() => this;
    [Inject] private Joystick joystick;
    [Inject] private AudioManager audioManager;
    [SerializeField] private Weapon weapon;
    [SerializeField] private PlayerSO playerSettings;

    private int killCount = 0;
    private int level = 0;
    private int experience = 0;
    private Rigidbody2D rigidBody;
    private Health health;

    public static UnityAction OnDied;
    public static UnityAction<int> OnKill;
    public static UnityAction<int> OnLevelUp;
    public static UnityAction<float> OnHealthChanged;
    public static UnityAction<float> OnExperienceChanged;

    public Health Health => health;
    public Weapon Weapon => weapon;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        health.Initialize(playerSettings.maxHealth);
        weapon.Initialize(playerSettings.weaponSettings);
    }

    private void OnEnable()
    {
        health.OnHealthChanged += v => OnHealthChanged?.Invoke(v);
        health.OnDied += () => OnDied?.Invoke();
        Enemy.OnEnemyDied += () => OnKill?.Invoke(++killCount);
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDied -= () => OnKill?.Invoke(++killCount);
        health.OnDied -= () => OnDied?.Invoke();
        health.OnHealthChanged -= v => OnHealthChanged?.Invoke(v);
    }

    private void FixedUpdate()
    {
        Movement();
        weapon.AimAndShoot(transform.position);
    }

    public void ChangeExperience(int amount)
    {
        experience += amount;

        var nextLevelExperience = level ^ 2 * playerSettings.levelUpExperience;
        if (experience >= nextLevelExperience)
        {
            experience -= nextLevelExperience;
            level++;
            OnLevelUp?.Invoke(level);

            audioManager.Play("LevelUp");
        }

        var experiencePercentage = (float)experience / nextLevelExperience;
        OnExperienceChanged?.Invoke(experiencePercentage);
    }

    private void Movement()
    {
        var velocity = joystick.InputVector * playerSettings.speed;
        rigidBody.velocity = velocity;
    }

}