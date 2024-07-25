using UnityEngine;
using Utilities;

namespace Gameplay
{
    public class Collectable : MonoBehaviour, ICollectable
    {
        [SerializeField] private int _amount;
        
        public void Collect(Wallet wallet)
        {
            wallet.Add(_amount);
            Destroy(gameObject);
        }
    }
}