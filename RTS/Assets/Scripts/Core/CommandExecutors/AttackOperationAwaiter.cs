using Abstractions;
using Commands;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;


public partial class AttackComandExecutor : CommandExecutorBase<IAttackCommand>
{

    public class AttackOperation : IAwaitable<AsyncExtensions.Void>
    {
        public class AttackOperationAwaiter : AwaiterBaseClass<AsyncExtensions.Void>
        {
            private AttackOperation _attackOperation;
            public AttackOperationAwaiter(AttackOperation attackOperation)
            {
                _attackOperation = attackOperation;
                attackOperation.OnComplete += onComplete;
            }
            private void onComplete()
            {
                _attackOperation.OnComplete -= onComplete;
                OnWaitFinish(new AsyncExtensions.Void());
            }
        }
        private event Action OnComplete;
        private readonly AttackComandExecutor _attackCommandExecutor;
        private readonly IAttackable _target;
        private bool _isCancelled;

        public AttackOperation(AttackComandExecutor attackCommandExecutor, IAttackable target)
            {
                _target = target;
                _attackCommandExecutor = attackCommandExecutor;
                var thread = new Thread(attackAlgorythm);
                thread.Start();
            }

            public void Cancel()
            {
                _isCancelled = true;
                OnComplete?.Invoke();
            }
            private void attackAlgorythm(object obj)
            {
                while (true)
                {
                    if (
                    _attackCommandExecutor == null
                    || _attackCommandExecutor._ourHealth.Health == 0
                    || _target.Health == 0
                    || _isCancelled
                    )
                    {
                        OnComplete?.Invoke();
                        return;
                    }
                    var targetPosition = default(Vector3);
                    var ourPosition = default(Vector3);
                    var ourRotation = default(Quaternion);
                    lock (_attackCommandExecutor)
                    {
                        targetPosition = _attackCommandExecutor._targetPosition;
                        ourPosition = _attackCommandExecutor._ourPosition;
                        ourRotation = _attackCommandExecutor._ourRotation;
                    }
                    var vector = targetPosition - ourPosition;
                    var distanceToTarget = vector.magnitude;
                    if (distanceToTarget > _attackCommandExecutor._attackingDistance)
                    {
                        var finalDestination = targetPosition - vector.normalized *
                        (_attackCommandExecutor._attackingDistance * 0.9f);
                        _attackCommandExecutor._targetPositions.OnNext(finalDestination);
                        Thread.Sleep(100);
                    }
                    else if (ourRotation != Quaternion.LookRotation(vector))
                    {
                        _attackCommandExecutor.
                        _targetRotations
                        .OnNext(Quaternion.LookRotation(vector));
                    }
                    else
                    {
                        _attackCommandExecutor._attackTargets.OnNext(_target);
                        Thread.Sleep(_attackCommandExecutor._attackingPeriod);
                    }
                }
            }
            public IAwaiter<AsyncExtensions.Void> GetAwaiter()
            {
                return new AttackOperationAwaiter(this);
            }
        }
    }
