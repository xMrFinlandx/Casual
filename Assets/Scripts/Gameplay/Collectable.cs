using UnityEngine;
using Utilities;

namespace Gameplay
{
    public class Collectable : MonoBehaviour, IInteractable
    {
        [SerializeField] private int _amount;
        
        public void Interact(Wallet wallet)
        {
            wallet.Add(_amount);
            Destroy(gameObject);
        }
    }
}