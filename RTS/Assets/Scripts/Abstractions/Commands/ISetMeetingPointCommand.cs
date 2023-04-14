using UnityEngine;

namespace Commands
{
    public interface ISetMeetingPointCommand : ICommand
    {
        public Vector3 Point { get; }
    }
}