using MyLibrary;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;
using Zenject;
using System;

public class Bootstrapper : MonoInstaller
{
    private DiContainer _container;
    public Bootstrapper(DiContainer container)
    {
        this._container = container;
    }

    public override void InstallBindings()
    {
        Debug.Log("Init - DI");

        Container.Bind<PrefabFactory>().AsSingle();

        IBindingInstaller[] installers = Container.FindAndCreateAllOfType<IBindingInstaller>();
        foreach (var installer in installers)
        {
            installer.InstallBindings(Container);
        }
    }
}
public interface IBindingInstaller
{
    public void InstallBindings(DiContainer diContainer);
}
