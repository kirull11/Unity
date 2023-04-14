using Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class StopComandExecutor : CommandExecutorBase<IStopCommand>
{
    public CancellationTokenSource CancellationTokenSource { get; set; }

    public override async Task ExecuteSpecificCommand(IStopCommand command)
    {
        CancellationTokenSource?.Cancel();
    }
}
