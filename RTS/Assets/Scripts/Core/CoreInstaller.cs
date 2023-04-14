using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CoreInstaller : MonoInstaller
{
    [SerializeField] private Sprite _chomperSprite;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<TimeModel>().AsSingle();

        Container.Bind<Sprite>().WithId("Chomper").FromInstance(_chomperSprite);

    }
}
