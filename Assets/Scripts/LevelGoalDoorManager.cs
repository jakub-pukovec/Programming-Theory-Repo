using UnityEngine;

public class LevelGoalDoorManager : MonoBehaviour
{
    private ScoreManager _scoreManager;

    private void Awake()
    {
        _scoreManager = ScoreManager.Instance;    
    }
    private void OnEnable()
    {
        _scoreManager.OnGoalReached += _scoreManager_OnGoalReached;
    }

    private void OnDisable()
    {
        _scoreManager.OnGoalReached -= _scoreManager_OnGoalReached;
    }

    private void _scoreManager_OnGoalReached()
    {
        OpenDoor();
    }

    private void OpenDoor()
    {
        gameObject.SetActive(false);
    }
}
