using DG.Tweening;
using Gameplay;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using Zenject;

namespace UI
{
    public class LevelProgressiveUI : MonoBehaviour
    {
        [SerializeField] private Transform _child;
        [SerializeField] private Gradient _gradient;
        [SerializeField] private Image _fillImage;
        [SerializeField] private TextMeshProUGUI _textMesh;

        private IWallet _wallet;

        private const float _IMAGE_FILL_DURATION = .2f;
        
        [Inject]
        private void Construct(IWallet wallet)
        {
            _wallet = wallet;
        }
        
        private void Start()
        {
            PlayerController.GameStartedAction += OnGameStarted;
            _child.gameObject.SetActive(false);
        }

        private void OnGameStarted()
        {
            _child.gameObject.SetActive(true);
            _wallet.BalanceChangedAction += OnBalanceChanged;
            OnBalanceChanged(_wallet.Balance, 0);
        }

        private void OnBalanceChanged(int current, int added)
        {
            var amount = (float)current / LevelGoalManager.Instance.Goal;
            DOTween.To(() => _fillImage.fillAmount, x => _fillImage.fillAmount = x, amount, _IMAGE_FILL_DURATION)
                .OnUpdate(() => _fillImage.color = _gradient.Evaluate(_fillImage.fillAmount));
            
            _textMesh.text = current.ToString();
        }

        private void OnDestroy()
        {
            _wallet.BalanceChangedAction -= OnBalanceChanged;
            PlayerController.GameStartedAction -= OnGameStarted;
        }
    }
}