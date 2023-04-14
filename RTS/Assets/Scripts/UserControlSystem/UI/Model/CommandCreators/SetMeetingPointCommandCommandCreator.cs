using System;
using UnityEngine;
using Zenject;

namespace Commands
{
    public class SetMeetingPointCommandCommandCreator : CancellableCommandCreatorBase<ISetMeetingPointCommand, Vector3>
    {
        protected override ISetMeetingPointCommand CreateCommand(Vector3 argument)
        {
            return new SetMeetingPointCommand(argument);
        }
    }
}