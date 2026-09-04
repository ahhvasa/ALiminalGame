using Zenject;

public class UpdateManagerInstaller : IBindingInstaller
{
    public void InstallBindings(DiContainer Container)
    {
        Container
        .Bind<UpdateManadger>()
        .FromComponentInHierarchy()
        .AsSingle();
    }
}