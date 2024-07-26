using DG.Tweening;
using Player;
using UnityEngine;
using Utilities;
using Zenject;

namespace Gameplay
{
    public class FinishDoor : MonoBehaviour, IInteractable
    {
        [SerializeField] private float _rotationDegree = 120;
        [SerializeField] private float _rotationDuration = .3f;
        [SerializeField] private int _targetCoinsAmount;
        [SerializeField] private Transform _leftDoor;
        [SerializeField] private Transform _rightDoor;

        public int TargetCoinsAmount => _targetCoinsAmount;
        
        private PlayerController _playerController;
        
        [Inject]
        private void Construct(PlayerInteraction playerInteraction)
        {
            _playerController = playerInteraction.PlayerController;
        }

        public void Interact(Wallet wallet)
        {
            if (wallet.Balance < LevelGoalManager.Instance.MinValue)
            {
                _playerController.SetLose();
                return;
            }

            if (_targetCoinsAmount > wallet.Balance)
            {
                _playerController.SetWin();
            }
            else
            {
                Open();
            }
        }

        private void Open()
        {
            _leftDoor.DORotate(_leftDoor.rotation.eulerAngles + new Vector3(0, _rotationDegree), _rotationDuration);
            _rightDoor.DORotate(_rightDoor.rotation.eulerAngles + new Vector3(0, -_rotationDegree), _rotationDuration);
        }
    }
}