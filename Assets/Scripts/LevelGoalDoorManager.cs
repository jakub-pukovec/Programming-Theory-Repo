using UnityEngine;

public class LevelGoalDoorManager : MonoBehaviour
{
    private ScoreManager _scoreManager;

    [SerializeField] private int _scoreGoal = 200;

    private void Awake()
    {
        _scoreManager = ScoreManager.Instance;    
    }
    private void OnEnable()
    {
        _scoreManager.OnScoreAdded += _scoreManager_OnScoreAdded;
    }

    private void OnDisable()
    {
        _scoreManager.OnScoreAdded -= _scoreManager_OnScoreAdded;
    }
    
    private void _scoreManager_OnScoreAdded(int totalScore)
    {
        if (totalScore >= _scoreGoal)
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        gameObject.SetActive(false);
    }
}
