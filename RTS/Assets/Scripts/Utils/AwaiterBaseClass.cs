using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AwaiterBaseClass<TAwaited> : IAwaiter<TAwaited>
{
    protected bool _isCompleted;
    protected Action _continuation;
    private TAwaited _result;

    public void OnCompleted(Action continuation)
    {
        if (_isCompleted)
        {
            continuation?.Invoke();
        }
        else
        {
            _continuation = continuation;
        }
    }
    public bool IsCompleted => _isCompleted;
    public TAwaited GetResult() => _result;

    protected void OnWaitFinish(TAwaited result)
    {
        _result = result;
        _isCompleted = true;
        _continuation?.Invoke();
    }

}
