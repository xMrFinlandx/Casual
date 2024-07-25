using UnityEngine;

namespace Scriptables.Gates
{
    [CreateAssetMenu(fileName = "New Door Config", menuName = "Gameplay/Door Config", order = 0)]
    public class DoorConfig : ScriptableObject
    {
        [SerializeField] private Material _material;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private string _text;
        [SerializeField] private int _pointsAmount;

        public Material Material => _material;
        public Sprite Sprite => _sprite;
        public string Text => _text;
        public int PointsAmount => _pointsAmount;
    }
}