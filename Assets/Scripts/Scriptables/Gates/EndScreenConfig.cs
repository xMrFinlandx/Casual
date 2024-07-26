using UnityEngine;

namespace Scriptables.Gates
{
    [CreateAssetMenu(fileName = "End Screen Config", menuName = "UI/End Screen Config", order = 0)]
    public class EndScreenConfig : ScriptableObject
    {
        [SerializeField] private string _winHeaderText;
        [SerializeField] private string _winButtonText;
        [SerializeField] private Sprite _winButtonSprite;
        [SerializeField] private Color _winHeaderColor;
        [SerializeField] private string _loseHeaderText;
        [SerializeField] private string _loseButtonText;
        [SerializeField] private Sprite _loseButtonSprite;
        [SerializeField] private Color _loseHeaderColor;

        public string WinHeaderText => _winHeaderText;
        public string WinButtonText => _winButtonText;
        public Sprite WinButtonSprite => _winButtonSprite;
        public Color WinHeaderColor => _winHeaderColor;

        public string LoseHeaderText => _loseHeaderText;
        public string LoseButtonText => _loseButtonText;
        public Sprite LoseButtonSprite => _loseButtonSprite;
        public Color LoseHeaderColor => _loseHeaderColor;
    }
}