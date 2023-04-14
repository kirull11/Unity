using Abstractions;
using Commands;
using System;
using System.Threading;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public partial class AttackComandExecutor : CommandExecutorBase<IAttackCommand>
{
    [SerializeField] private Animator _animator;
    [SerializeField] private StopComandExecutor _stopCommandExecutor;
    [Inject] private IHealthHolder _ourHealth;
    [Inject(Id = "AttackDistance")] private float _attackingDistance;
    [Inject(Id = "AttackPeriod")] private int _attackingPeriod;
    private Vector3 _ourPosition;
    private Vector3 _targetPosition;
    private Quaternion _ourRotation;
    private readonly Subject<Vector3> _targetPositions = new Subject<Vector3>();
    private readonly Subject<Quaternion> _targetRotations = new Subject<Quaternion>();
    private readonly Subject<IAttackable> _attackTargets = new Subject<IAttackable>();
    private Transform _targetTransform;
    private AttackOperation _currentAttackOp;
    [Inject]
    private void Init()
    {
        _targetPositions
        .Select(value => new Vector3((float)Math.Round(value.x, 2),
        (float)Math.Round(value.y, 2), (float)Math.Round(value.z, 2)))
        .Distinct()
        .ObserveOnMainThread()
        .Subscribe(startMovingToPosition);
        _attackTargets
        .ObserveOnMainThread()
        .Subscribe(startAttackingTargets);
        _targetRotations
        .ObserveOnMainThread()
        .Subscribe(setAttackRoation);
    }


    private void setAttackRoation(Quaternion targetRotation)
    {
        transform.rotation = targetRotation;
    }
    private void startAttackingTargets(IAttackable target)
    {
        GetComponent<NavMeshAgent>().isStopped = true;
        GetComponent<NavMeshAgent>().ResetPath();
        _animator.SetTrigger(Animator.StringToHash("Attack"));
        target.RecieveDamage(GetComponent<IDamageDealer>().Damage);
    }
    private void startMovingToPosition(Vector3 position)
    {
        GetComponent<NavMeshAgent>().destination = position;
        _animator.SetTrigger(Animator.StringToHash("Walk"));
    }
    public override async Task ExecuteSpecificCommand(IAttackCommand command)
    {
        _targetTransform = (command.Target as Component).transform;
        _currentAttackOp = new AttackOperation(this, command.Target);
        Update();
        _stopCommandExecutor.CancellationTokenSource = new CancellationTokenSource();
        try
        {
            await
            _currentAttackOp.WithCancellation(_stopCommandExecutor.CancellationTokenSource.Token);
        }
        catch
        {
            _currentAttackOp.Cancel();
        }
        _animator.SetTrigger("Idle");
        _currentAttackOp = null;
        _targetTransform = null;
        _stopCommandExecutor.CancellationTokenSource = null;
    }

    private void Update()
    {
        if (_currentAttackOp == null)
        {
            return;
        }
        lock (this)
        {
            _ourPosition = transform.position;
            _ourRotation = transform.rotation;
            if (_targetTransform != null)
            {
                _targetPosition = _targetTransform.position;
            }
        }
    }
}

