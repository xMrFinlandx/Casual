using Gameplay;
using Player;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Gradient _gradient;
        [SerializeField] private Image _fillImage;

        private IWallet _wallet;
        
        private void Start()
        {
            _wallet = FindObjectOfType<PlayerInteraction>().Wallet;

            _wallet.BalanceChangedAction += OnBalanceChanged;
        }

        private void OnBalanceChanged(int current, int added)
        {
            var amount = (float) current / LevelGoalManager.Instance.Goal;
            _fillImage.fillAmount = amount;
            _fillImage.color = _gradient.Evaluate(amount);
        }

        private void OnDestroy()
        {
            _wallet.BalanceChangedAction -= OnBalanceChanged;
        }
    }
}