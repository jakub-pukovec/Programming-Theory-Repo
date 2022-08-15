using TMPro;
using UnityEngine;

//Abstraction
//Inheritance in all derived classes plus also in this one
public abstract class Fruit : MonoBehaviour
{
    [SerializeField] Vector2 _infoPanelSize = new Vector2(200, 85);

    private TMP_Text _scoreText;
    private ScoreManager _scoreManager;
    private ItemInfoUIManager _infoPanel;

    protected abstract int ScorePoints { get; }
    protected abstract string Name { get; }

    private void Awake()
    {
        _scoreText = GameObject.Find("OverlayCanvas").transform.Find("ScoreText").GetComponent<TMP_Text>();
    }

    private void Start()
    {
        _scoreManager = ScoreManager.Instance;
        _infoPanel = ItemInfoUIManager.Instance;
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
        _infoPanel.SetPosition(transform.position + Vector3.up * .35f);
        _infoPanel.SetSize(_infoPanelSize.x, _infoPanelSize.y);
        _infoPanel.Show();
        _infoPanel.SetTitleAndDescription(Name, $"Adds to Score: {ScorePoints}");
    }

    private void OnMouseExit()
    {
        _infoPanel.Hide();
    }

    private void OnDisable()
    {
        if (_infoPanel != null)
        {
            _infoPanel.Hide();
        }
    }
}
