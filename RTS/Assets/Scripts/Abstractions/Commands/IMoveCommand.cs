using UnityEngine;

namespace Commands
{
    public interface IMoveCommand : ICommand
    {
        public Vector3 Target { get; }
    }
}