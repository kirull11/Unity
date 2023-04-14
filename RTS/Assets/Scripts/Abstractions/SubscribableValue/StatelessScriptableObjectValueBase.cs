using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class StatelessScriptableObjectValueBase <T> : SubscribableValueBaseClass<T>, IObservable<T>
{
    private Subject<T> _innerDataSource = new Subject<T>();
    
    public override void SetValue(T value)
    {
        base.SetValue(value);
        _innerDataSource.OnNext(value);
    }


    public IDisposable Subscribe(IObserver<T> observer) =>
        _innerDataSource.Subscribe(observer);
}
