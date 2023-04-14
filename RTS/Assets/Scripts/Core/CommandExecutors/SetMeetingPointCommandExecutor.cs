using Commands;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class SetMeetingPointCommandExecutor : CommandExecutorBase<ISetMeetingPointCommand>
{
    [Inject]
    private CommandExecutorBase<IProduceUnitCommand> _produceUnitCommandExecutor;

    public override async Task ExecuteSpecificCommand(ISetMeetingPointCommand command)
    {
        if (_produceUnitCommandExecutor is IHaveMeetingPoint mp)
            mp.MeetingPoint = command.Point;
    }
}
