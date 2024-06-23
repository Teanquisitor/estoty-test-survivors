public class AmmoDrop : Loot
{
    public override void Collect()
    {
        Target.Weapon.ChangeAmmo(Value);
        base.Collect();
    }
}