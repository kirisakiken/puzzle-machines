using System;
using System.Threading.Tasks;

using KirisakiTechnologies.GameSystem.Scripts.Controllers;

using UnityEngine;

namespace KirisakiTechnologies.PuzzleMachines.Scripts.Controllers.Machines.Doors
{
    public class BinaryDoor : GameControllerBaseMono, IBinaryDoor
    {
        #region IDoor Implementation

        public Transform Hinge => _Hinge;
        public Transform DoorTransform => _DoorTransform;

        #endregion

        #region IBinaryDoor Implementation

        public BinaryDoorState State
        {
            get => _State;
            private set => _State = value;
        }

        public Quaternion OpenAngle => Quaternion.Euler(_OpenAngle);
        public Quaternion CloseAngle => Quaternion.Euler(_CloseAngle);

        public async Task ChangeState()
        {
            if (_StateChangeTask?.IsCompleted == false)
                return;

            _StateChangeTask = ChangeStateInternal();
            await _StateChangeTask;
        }

        #endregion

        #region Private

        [SerializeField]
        private BinaryDoorState _State = BinaryDoorState.Close;

        [SerializeField]
        private Vector3 _OpenAngle = new Vector3(0, 80, 0);

        [SerializeField]
        private Vector3 _CloseAngle = Vector3.zero;

        [SerializeField]
        private Transform _Hinge;

        [SerializeField]
        private Transform _DoorTransform;

        [SerializeField]
        private float _ActionSpeed = 10.0f;

        private Task _StateChangeTask;

        private async Task ChangeStateInternal()
        {
            var startAngle = Hinge.rotation;
            var finishAngle = State == BinaryDoorState.Open
                ? CloseAngle
                : OpenAngle;

            var t = 0.0f;
            while (t <= 1)
            {
                Hinge.rotation = Quaternion.Lerp(startAngle, finishAngle, t);
                t += Time.deltaTime * _ActionSpeed;
                await Task.Yield();
            }

            State = State == BinaryDoorState.Open
                ? BinaryDoorState.Close
                : BinaryDoorState.Open;
        }

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            if (_Hinge == null)
                throw new ArgumentNullException(nameof(_Hinge));

            if (_DoorTransform == null)
                throw new ArgumentNullException(nameof(_DoorTransform));

            Hinge.rotation = State == BinaryDoorState.Open
                ? OpenAngle
                : CloseAngle;
        }

        #endregion
    }
}
