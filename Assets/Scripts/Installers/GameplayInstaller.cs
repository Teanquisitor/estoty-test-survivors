using Zenject;

public class GameplayInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Joystick>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Player>().FromComponentInHierarchy().AsSingle();
        Container.Bind<MainCamera>().FromComponentInHierarchy().AsSingle();
        Container.Bind<AudioManager>().FromComponentInHierarchy().AsSingle();
    }
}