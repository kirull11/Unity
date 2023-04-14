using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIViewInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
        .Bind<BottomCenterView>()
        .FromComponentInHierarchy()
        .AsSingle();
    }
}
