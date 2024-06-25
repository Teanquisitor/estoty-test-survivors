using Zenject;

public class GameplayInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Joystick>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<Player>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<MainCamera>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<AudioManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<QuestsManager>().FromComponentInHierarchy().AsSingle().NonLazy();
    }

}