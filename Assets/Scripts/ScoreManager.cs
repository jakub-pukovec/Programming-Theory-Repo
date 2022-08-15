using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public event Action OnGoalReached;

    [SerializeField] private int _scoreGoal = 125;
    private bool _goalReached;

    public static ScoreManager Instance { get; private set; }
    public int Score { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void AddScore(int scoreToAdd)
    {
        Score += scoreToAdd;
        if (Score >= _scoreGoal && !_goalReached)
        {
            _goalReached = true;
            OnGoalReached?.Invoke();
        }
    }
}