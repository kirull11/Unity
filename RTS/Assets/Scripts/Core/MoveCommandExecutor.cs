using Commands;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;


public class MoveCommandExecutor : CommandExecutorBase<IMoveCommand>
{
    [SerializeField] private UnitMovementStop _stop;
    [SerializeField] private Animator _animator;
    public override async Task ExecuteSpecificCommand(IMoveCommand command)
    {
        GetComponent<NavMeshAgent>().destination = command.Target;
        _animator.SetTrigger("Walk");
        await _stop;
        _animator.SetTrigger("Idle");
    }
}

