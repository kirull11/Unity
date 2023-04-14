using Abstractions;
using Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UserControlSystem;
using Zenject;
using ISelectable = Abstractions.ISelectable;

public class MouseClickCommandPresenter : MonoBehaviour
{

    [SerializeField] private SelectableValue _selectedObject;

    [Inject] private CommandCreatorBase<IMoveCommand> _mover;

    private void Start()
    {
        _selectedObject.OnNewValue += CheckType;
    }

    private void CheckType(ISelectable obj)
    {
        if (obj == null)
        {
            return;
        }
        if ((obj as Component).TryGetComponent<CommandExecutorBase<IMoveCommand>>(out var b))
        {
            var queue = (obj as Component).GetComponentInParent<ICommandsQueue>();
            _mover.ProcessCommandExecutor(b, command => ExecuteCommandWrapper(command, queue));
        }
    }

    private void OnDestroy()
    {
        _selectedObject.OnNewValue -= CheckType;

    }

    public void ExecuteCommandWrapper(object command, ICommandsQueue commandsQueue)
    {
        if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
        {
            commandsQueue.Clear();
        }
        commandsQueue.EnqueueCommand(command);
        CheckType(_selectedObject.CurrentValue);
    }
}
