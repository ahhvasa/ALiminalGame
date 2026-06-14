using Zenject;

public class SaveSystemInstaller : IBindingInstaller
{
    public void InstallBindings(DiContainer diContainer)
    {
        diContainer.BindInterfacesAndSelfTo<JsonFileSaveSystem>().AsSingle();
    }
}