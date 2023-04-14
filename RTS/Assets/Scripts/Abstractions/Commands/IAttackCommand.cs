using Abstractions;
using UnityEngine;

namespace Commands
{
    public interface IAttackCommand: ICommand
    {
        public IAttackable Target { get; }
    }
}