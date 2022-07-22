using UnityEngine;

namespace KirisakiTechnologies.PuzzleMachines.Scripts.Controllers.Machines.Doors
{
    public interface IDoor
    {
        Transform Hinge { get; }

        Transform DoorTransform { get; }
    }
}
