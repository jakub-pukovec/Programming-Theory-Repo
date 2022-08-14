using TMPro;
using UnityEngine;

//Abstraction
public abstract class Fruit : MonoBehaviour
{
    private TMP_Text _scoreText;
    private TMP_Text _infoTitleText;
    private TMP_Text _infoScoreText;
    private GameObject _itemInfo;
    private ScoreManager _scoreManager;

    protected abstract int ScorePoints { get; }
    protected abstract string Name { get; }

    private void Awake()
    {
        _scoreText = GameObject.Find("ScoreCanvas").transform.Find("ScoreText").GetComponent<TMP_Text>();
        _itemInfo = GameObject.Find("ItemInfo");
        _infoTitleText = _itemInfo.transform.Find("TitleText").GetComponent<TMP_Text>();
        _infoScoreText = _itemInfo.transform.Find("ScoreText").GetComponent<TMP_Text>();
    }

    private void Start()
    {
        _itemInfo.SetActive(false);
        _scoreManager = ScoreManager.Instance;
    }

    public void Pickup()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        UpdateScore();
        DisplayScore();
        gameObject.SetActive(false);
    }

    private void UpdateScore()
    {
        _scoreManager.AddScore(ScorePoints);
    }

    private void DisplayScore()
    {
        _scoreText.text = $"Score: { _scoreManager.Score }";
    }

    private void OnMouseEnter()
    {
        _itemInfo.transform.position = transform.position + Vector3.up * .35f;
        _itemInfo.SetActive(true);
        _infoTitleText.text = Name;
        _infoScoreText.text = $"Adds to Score: {ScorePoints}";
    }

    private void OnMouseExit()
    {
        _itemInfo.SetActive(false);
    }

    private void OnDisable()
    {
        if (_itemInfo != null)
        {
            _itemInfo.SetActive(false);
        }
    }
}
