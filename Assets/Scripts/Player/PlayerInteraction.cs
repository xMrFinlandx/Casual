using System;
using UnityEngine;
using Utilities;
using Zenject;

namespace Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private int _initialBalance = 40;

        private Wallet _wallet;
        
        public PlayerController PlayerController => _playerController;
        public IWallet Wallet => _wallet;

        [Inject]
        private void Construct()
        {
            _wallet = new Wallet(_initialBalance);
        }

        private void Start()
        {
            _wallet.BalanceIsZeroAction += OnBalanceIsZero;
        }

        private void OnBalanceIsZero()
        {
            _playerController.SetLose();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IInteractable>(out var collectable))
            {
                print(_wallet == null);
                collectable.Interact(_wallet);
            }
        }

        private void OnDestroy()
        {
            _wallet.BalanceIsZeroAction -= OnBalanceIsZero;
        }
    }
}