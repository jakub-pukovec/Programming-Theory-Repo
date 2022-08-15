using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleSceneButtonsManager : MonoBehaviour
{
    [SerializeField] Button _startButton;
    [SerializeField] Button _exitButton;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(TitleSceneManager.Instance.StartGame);
        _exitButton.onClick.AddListener(TitleSceneManager.Instance.ExitGame);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(TitleSceneManager.Instance.StartGame);
        _exitButton.onClick.RemoveListener(TitleSceneManager.Instance.ExitGame);
    }
}