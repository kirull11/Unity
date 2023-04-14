using Abstractions;
using Commands;
using System;
using UnityEngine;
using UserControlSystem;
using Zenject;

public class UiModelInstaller : MonoInstaller
{
    [SerializeField] private AssetsContext _legacyContext;
    [SerializeField] private Vector3Value _vector3Value;
    [SerializeField] private SelectableValue _selectable;


    [SerializeField] private AttackableValue _attackableValue;

    public override void InstallBindings()
    {
        Container.Bind<AssetsContext>().FromInstance(_legacyContext);
        Container.Bind<Vector3Value>().FromInstance(_vector3Value);

        Container.Bind<SelectableValue>().FromInstance(_selectable);
        Container.Bind<AttackableValue>().FromInstance(_attackableValue);

        Container.Bind<IAwaitable<IAttackable>>().FromInstance(_attackableValue);
        Container.Bind<IAwaitable<Vector3>>().FromInstance(_vector3Value);

        Container.Bind<IObservable<ISelectable>>().FromInstance(_selectable);

        Container.Bind<CommandCreatorBase<IProduceUnitCommand>>()
        .To<ProduceUnitCommandCommandCreator>().AsTransient();
        Container.Bind<CommandCreatorBase<IAttackCommand>>()
        .To<AttackCommandCommandCreator>().AsTransient();
        Container.Bind<CommandCreatorBase<IMoveCommand>>()
        .To<MoveCommandCommandCreator>().AsTransient();
        Container.Bind<CommandCreatorBase<IPatrolCommand>>()
        .To<PatrolCommandCommandCreator>().AsTransient();
        Container.Bind<CommandCreatorBase<IStopCommand>>()
        .To<StopCommandCommandCreator>().AsTransient();
        Container.Bind<CommandCreatorBase<ISetMeetingPointCommand>>()
        .To<SetMeetingPointCommandCommandCreator>().AsTransient();

        Container.Bind<CommandButtonsModel>().AsSingle();
        Container.Bind<BottomCenterModel>().AsSingle();

        Container.Bind<float>().WithId("Chomper").FromInstance(5f);
        Container.Bind<string>().WithId("Chomper").FromInstance("Chomper");
    }
}
