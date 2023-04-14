using Commands;
using UnityEngine;

public class SetMeetingPointCommand : ISetMeetingPointCommand
{
    public Vector3 Point { get; }

    public SetMeetingPointCommand(Vector3 point)
    {
        Point = point;
    }

}
