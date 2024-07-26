using Player;
using Scriptables.Gates;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class EndScreen : MonoBehaviour
    {
        [SerializeField] private Transform _child;
        [SerializeField] private TextMeshProUGUI _header;
        [SerializeField] private TextMeshProUGUI _buttonTextMesh;
        [SerializeField] private Button _button;
        [SerializeField] private EndScreenConfig _endScreenConfig;

        private void Start()
        {
            PlayerController.GameEndedAction += OnGameEnded;
            _child.gameObject.SetActive(false);
        }

        private void OnGameEnded(bool isWin)
        {
            _child.gameObject.SetActive(true);

            if (isWin)
            {
                _header.text = _endScreenConfig.WinHeaderText;
                _header.color = _endScreenConfig.WinHeaderColor;
                _buttonTextMesh.text = _endScreenConfig.WinButtonText;
                _button.image.sprite = _endScreenConfig.WinButtonSprite;
                _button.onClick.AddListener(LoadNextScene);
            }
            else
            {
                _header.text = _endScreenConfig.LoseHeaderText;
                _header.color = _endScreenConfig.LoseHeaderColor;
                _buttonTextMesh.text = _endScreenConfig.LoseButtonText;
                _button.image.sprite = _endScreenConfig.LoseButtonSprite;
                _button.onClick.AddListener(ReloadScene);
            }
        }

        private static void ReloadScene()
        {
            var index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadSceneAsync(index);
        }

        private static void LoadNextScene()
        {
            var index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadSceneAsync(index + 1);
        }

        private void OnDisable()
        {
            PlayerController.GameEndedAction -= OnGameEnded;
            _button.onClick.RemoveAllListeners();
        }
    }
}