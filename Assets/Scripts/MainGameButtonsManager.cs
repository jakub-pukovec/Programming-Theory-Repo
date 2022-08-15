using UnityEngine;
using UnityEngine.UI;

public class MainGameButtonsManager : MonoBehaviour
{
    [SerializeField] private Button _backToMenuButon;

    private void OnEnable()
    {
        if (TitleSceneManager.Instance != null)
        {
            _backToMenuButon.onClick.AddListener(TitleSceneManager.Instance.GoToMainMenu);
        }
    }

    private void OnDisable()
    {
        if (TitleSceneManager.Instance != null)
        {
            _backToMenuButon.onClick.RemoveListener(TitleSceneManager.Instance.GoToMainMenu);
        }
    }
}