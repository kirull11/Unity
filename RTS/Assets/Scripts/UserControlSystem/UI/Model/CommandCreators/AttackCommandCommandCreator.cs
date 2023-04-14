using Abstractions;
using System;
using System.Threading;
using UserControlSystem;
using Zenject;

namespace Commands
{
    public class AttackCommandCommandCreator : CancellableCommandCreatorBase<IAttackCommand, IAttackable>
    {
        [Inject] private AssetsContext _context;
        [Inject] private AttackableValue _attackableValue;
        private CancellationTokenSource _ctSource;

        protected override IAttackCommand CreateCommand(IAttackable argument) => new AttackCommand(argument);


    }
}