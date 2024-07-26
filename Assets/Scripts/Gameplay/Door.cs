using Scriptables.Gates;
using TMPro;
using UnityEngine;
using Utilities;

namespace Gameplay
{
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] private DoorConfig _doorConfig;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private TextMeshPro _textMesh;
        [SerializeField] private BoxCollider _collider;
        [SerializeField] private Door[] _linked;

        private int _pointsAmount;
        
        public void Interact(Wallet wallet)
        {
            wallet.Add(_pointsAmount);
            
            foreach (var door in _linked)
            {
                door._collider.enabled = false;
            }

            _collider.enabled = false;
        }
        
        private void OnValidate()
        {
            if (_doorConfig == null)
                return;
            
            _spriteRenderer.sprite = _doorConfig.Sprite;
            _meshRenderer.material = _doorConfig.Material;
            _textMesh.text = _doorConfig.Text;
            _pointsAmount = _doorConfig.PointsAmount;
        }

        private void Start()
        {
            _collider.enabled = true;
        }
    }
}