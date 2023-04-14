using Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;

public class BottomCenterModel
{
    public IObservable<IUnitProducer> UnitProducers { get; private set; }

    [Inject]
    public void Init(IObservable<ISelectable> currentlySelected)
    {
        UnitProducers = currentlySelected
        .Select(selectable => selectable as Component)
        .Select(component => component?.GetComponent<IUnitProducer>());
    }
}
