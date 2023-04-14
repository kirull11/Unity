using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Zenject;

namespace Commands
{
    public abstract class CancellableCommandCreatorBase <TCommand, TArgument> : CommandCreatorBase<TCommand> where TCommand : class, ICommand
    {
        [Inject] private AssetsContext _context;
        [Inject] private IAwaitable<TArgument> _awaitableArgument;
        private CancellationTokenSource _ctSource;

        protected override sealed async void ClassSpecificCommandCreation(Action<TCommand> creationCallback)
        {
            _ctSource = new CancellationTokenSource();
            try
            {
                var argument = await _awaitableArgument.WithCancellation(_ctSource.Token);
                creationCallback?
                .Invoke(_context.Inject(CreateCommand(argument)));
            }
            catch { }
        }
        protected abstract TCommand CreateCommand(TArgument argument);
        public override void ProcessCancel()
        {
            base.ProcessCancel();
            if (_ctSource != null)
            {
                _ctSource.Cancel();
                _ctSource.Dispose();
                _ctSource = null;
            }
        }

    }
}