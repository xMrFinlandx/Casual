using UnityEngine;
using Utilities;

namespace Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private int _initialBalance = 40;

        private Wallet _wallet;

        public IWallet Wallet => _wallet;
        
        private void Awake()
        {
            _wallet = new Wallet(_initialBalance);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ICollectable>(out var collectable))
                collectable.Collect(_wallet);
        }
    }
}