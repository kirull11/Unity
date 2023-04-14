using Commands;
using Core;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;

public class ProduceUnitComandExecutor : CommandExecutorBase<IProduceUnitCommand>, IUnitProducer, IHaveMeetingPoint
{

    public IReadOnlyReactiveCollection<IUnitProductionTask> Queue => _queue;

    public Vector3 MeetingPoint { get; set; }

    [SerializeField] private Transform _unitsParent;
    [SerializeField] private int _maximumUnitsInQueue = 6;

    private ReactiveCollection<IUnitProductionTask> _queue = new ReactiveCollection<IUnitProductionTask>();
    [Inject] private DiContainer _diContainer;

    private void Update()
    {
        if (_queue.Count == 0)
        {
            return;
        }
        var innerTask = (UnitProductionTask)_queue[0];
        innerTask.TimeLeft -= Time.deltaTime;
        if (innerTask.TimeLeft <= 0)
        {
            removeTaskAtIndex(0);

            var instance = _diContainer.InstantiatePrefab(innerTask.UnitPrefab, new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)), Quaternion.identity, _unitsParent);
            
            var factionMember = instance.GetComponent<FactionMember>();
            factionMember.SetFaction(GetComponent<FactionMember>().FactionId);

            var queue = instance.GetComponent<ICommandsQueue>();
            queue.EnqueueCommand(new MoveCommand(MeetingPoint));
        }
    }

    public void Cancel(int index) => removeTaskAtIndex(index);
    private void removeTaskAtIndex(int index)
    {
        for (int i = index; i < _queue.Count - 1; i++)
            {
            _queue[i] = _queue[i + 1];
            }
        _queue.RemoveAt(_queue.Count - 1);
    }

    public override async Task ExecuteSpecificCommand(IProduceUnitCommand command)
    {
         _queue.Add(new UnitProductionTask(command.Icon, command.ProductionTime, command.UnitName, command.UnitPrefab));

    }
}
