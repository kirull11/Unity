using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UserControlSystem;
using Zenject;

namespace Commands
{
    public class MoveCommandCommandCreator : CancellableCommandCreatorBase<IMoveCommand, Vector3>
    {
        protected override IMoveCommand CreateCommand(Vector3 argument) => new
MoveCommand(argument);

    }
}