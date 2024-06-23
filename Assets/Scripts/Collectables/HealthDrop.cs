public class HealthDrop : Loot
{
    public override void Collect()
    {
        Target.Health.ChangeHealth(Value);
        base.Collect();
    }
}