using UnityEngine;
using UnityEngine.UI;

public class EndGameButtonsManager : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;

    private void OnEnable()

    {
        if (TitleSceneManager.Instance != null)
        {
            _startButton.onClick.AddListener(TitleSceneManager.Instance.StartGame);
            _exitButton.onClick.AddListener(TitleSceneManager.Instance.ExitGame);
        }
    }

    private void OnDisable()
    {
        if (TitleSceneManager.Instance != null)
        {
            _startButton.onClick.RemoveListener(TitleSceneManager.Instance.StartGame);
            _exitButton.onClick.RemoveListener(TitleSceneManager.Instance.ExitGame);
        }
    }
}