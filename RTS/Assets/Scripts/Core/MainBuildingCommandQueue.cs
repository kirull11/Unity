using Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainBuildingCommandQueue : MonoBehaviour, ICommandsQueue
{
    [Inject]
    CommandExecutorBase<IProduceUnitCommand> _produceUnitCommandExecutor;

    [Inject]
    CommandExecutorBase<ISetMeetingPointCommand> _setMeetingPointCommandExecutor;

    public void Clear() { }

    public async void EnqueueCommand(object command)
    {
        await _produceUnitCommandExecutor.TryExecuteCommand(command);
        await _setMeetingPointCommandExecutor.TryExecuteCommand(command);
    }

}
