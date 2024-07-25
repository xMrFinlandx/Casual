using DG.Tweening;
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
            var amount = (float)current / LevelGoalManager.Instance.Goal;
            DOTween.To(() => _fillImage.fillAmount, x => _fillImage.fillAmount = x, amount, .2f)
                .OnUpdate(() => _fillImage.color = _gradient.Evaluate(_fillImage.fillAmount));
        }

        private void OnDestroy()
        {
            _wallet.BalanceChangedAction -= OnBalanceChanged;
        }
    }
}