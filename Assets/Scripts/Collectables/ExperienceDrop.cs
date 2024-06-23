public class ExperienceDrop : Loot
{
    public override void Collect()
    {
        Target.ChangeExperience(Value);
        base.Collect();
    }
}