using System.Threading.Tasks;

using UnityEngine;

namespace KirisakiTechnologies.PuzzleMachines.Scripts.Controllers.Machines.Doors
{
    public enum BinaryDoorState
    {
        Open = 0,
        Close = 1,
    }

    public interface IBinaryDoor : IDoor
    {
        BinaryDoorState State { get; }

        Quaternion OpenAngle { get; }

        Quaternion CloseAngle { get; }

        Task ChangeState();
    }
}