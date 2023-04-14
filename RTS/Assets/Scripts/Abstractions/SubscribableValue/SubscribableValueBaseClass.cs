using System;
using UnityEngine;

public abstract class SubscribableValueBaseClass <T> : ScriptableObject,  IAwaitable<T>
{
    public class NewValueNotifier<TAwaited> : AwaiterBaseClass<TAwaited>
    {
        private readonly SubscribableValueBaseClass<TAwaited> _scriptableObjectValueBase;

        public NewValueNotifier(SubscribableValueBaseClass<TAwaited> scriptableObjectValueBase)
        {
            _scriptableObjectValueBase = scriptableObjectValueBase;
            _scriptableObjectValueBase.OnNewValue += OnNewValue;
        }

        private void OnNewValue(TAwaited obj)
        {
            _scriptableObjectValueBase.OnNewValue -= OnNewValue;
            OnWaitFinish(obj);
        }

        //public override TAwaited GetResult() => _result;
    }


    public T CurrentValue { get; private set; }
    public Action<T> OnNewValue;

    public virtual void SetValue(T value)
    {
        CurrentValue = value;
        OnNewValue?.Invoke(value);
    }

    public IAwaiter<T> GetAwaiter()
    {
        return new NewValueNotifier<T>(this);
    }

}
