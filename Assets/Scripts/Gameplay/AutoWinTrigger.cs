using Player;
using UnityEngine;
using Utilities;
using Zenject;

namespace Gameplay
{
    public class AutoWinTrigger : MonoBehaviour, IInteractable
    {
        private PlayerController _playerController;
        
        [Inject]
        private void Construct(PlayerInteraction playerInteraction)
        {
            _playerController = playerInteraction.PlayerController;
        }
        
        public void Interact(Wallet wallet)
        {
            _playerController.SetWin();
        }
    }
}