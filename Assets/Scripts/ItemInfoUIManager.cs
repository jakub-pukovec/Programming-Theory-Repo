using TMPro;
using UnityEngine;

//pseudo singleton
//encapsulation
//abstraction - refactored duplicate code from multiple classes to single robust independent functionality
public class ItemInfoUIManager : MonoBehaviour
{
    public static ItemInfoUIManager Instance { get; private set; }

    private RectTransform _rectTransform;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _description;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _rectTransform = GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }

    public void SetTitleAndDescription(string title, string description)
    {
        SetTitle(title);
        SetDescription(description);
    }

    public void SetTitle(string title)
    {
        _title.text = title;
    }

    public void SetDescription(string description)
    {
        _description.text = description;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetSize(float width, float height)
    {
        _rectTransform.sizeDelta = new Vector2(width, height);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}