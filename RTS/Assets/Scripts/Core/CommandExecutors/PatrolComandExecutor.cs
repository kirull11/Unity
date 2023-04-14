using Commands;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class PatrolComandExecutor : CommandExecutorBase<IPatrolCommand>
{
    [SerializeField] private UnitMovementStop _stop;
    [SerializeField] private Animator _animator;
    [SerializeField] private StopComandExecutor _stopCommandExecutor;

    public override async Task ExecuteSpecificCommand(IPatrolCommand command)
    {
        var position1 = command.From;
        var position2 = command.To;

        while (true)
        {
            var currentDestination = position1;

            GetComponent<NavMeshAgent>().destination = currentDestination;
            _animator.SetTrigger("Walk");
            _stopCommandExecutor.CancellationTokenSource = new CancellationTokenSource();
            try
            {
                await _stop
                .WithCancellation(
                    _stopCommandExecutor
                    .CancellationTokenSource
                    .Token);
            }
            catch
            {
                GetComponent<NavMeshAgent>().isStopped = true;
                GetComponent<NavMeshAgent>().ResetPath();
                break;
            }

            position1 = position2;
            position2 = currentDestination;

        }

        Debug.Log($"Patrol comand executed");
    }

    /*
     *  GetComponent<NavMeshAgent>().destination = command.Target;
        _animator.SetTrigger("Walk");
        _stopCommandExecutor.CancellationTokenSource = new CancellationTokenSource();
        try
        {
            await _stop
            .WithCancellation(
                _stopCommandExecutor
                .CancellationTokenSource
                .Token);
        }
        catch
        {
            GetComponent<NavMeshAgent>().isStopped = true;
            GetComponent<NavMeshAgent>().ResetPath();
        }
        _stopCommandExecutor.CancellationTokenSource = null;
        _animator.SetTrigger("Idle");
     */

}
