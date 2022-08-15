using TMPro;
using UnityEngine;

public class SwithController : MonoBehaviour
{
    [SerializeField] GameObject _switchable;

    private ISwitchable _switchableScript;
    private bool _isInTriggerZone;
    private GameObject _itemInfo;
    private TMP_Text _infoTitleText;
    private TMP_Text _infoDescriptionText;

    private void Awake()
    {
        _itemInfo = GameObject.Find("ItemInfo");
        _infoTitleText = _itemInfo.transform.Find("TitleText").GetComponent<TMP_Text>();
        _infoDescriptionText = _itemInfo.transform.Find("ScoreText").GetComponent<TMP_Text>();

        _switchableScript = _switchable.GetComponent<ISwitchable>();
        if (_switchableScript == null)
        {
            Debug.LogError("Switchable game object must implement ISwitchable interface.");
            return;
        }
    }

    private void Start()
    {
        _itemInfo.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Use") && _isInTriggerZone)
        {
            if (_switchableScript.IsSwitchedOn)
            {
                _switchableScript.SwitchOff();
            }
            else
            {
                _switchableScript.SwitchOn();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isInTriggerZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isInTriggerZone = false;
        }
    }

    private void OnMouseEnter()
    {
        _itemInfo.transform.position = transform.position + Vector3.up * .35f;
        _itemInfo.SetActive(true);
        _infoTitleText.text = "Platform Switch";
        _infoDescriptionText.text = "Hit Use button [E] when touching the switch to switch platform on / off.";
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