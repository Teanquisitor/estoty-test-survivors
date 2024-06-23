using UnityEngine;

public class Loot : MonoBehaviour
{
    [SerializeField] private LootSO settings;
    private Player target;
    public Player Target => target;
    public int Value => settings.value;

    private void FixedUpdate()
    {
        if (target == null || Vector2.Distance(transform.position, target.transform.position) > settings.attractionRadius)
            return;

        var direction = (target.transform.position - transform.position).normalized;
        transform.Translate(direction * settings.attractionSpeed * Time.fixedDeltaTime);

        if (Vector2.Distance(transform.position, target.transform.position) < settings.collectRadius)
            Collect();
    }

    public void Initialize(Player target) => this.target = target;

    public virtual void Collect() => Destroy(gameObject);
}