using System;
using TMPro;
using UnityEngine;

public class SwithController : MonoBehaviour
{
    [SerializeField] GameObject _switchable;
    [SerializeField] Vector2 _infoPanelSize = new Vector2(240, 125);

    private ISwitchable _switchableScript;
    private bool _isInTriggerZone;
    private ItemInfoUIManager _infoPanel;

    private void Awake()
    {
        _switchableScript = _switchable.GetComponent<ISwitchable>();
        if (_switchableScript == null)
        {
            Debug.LogError("Switchable game object must implement ISwitchable interface.");
            return;
        }
    }

    private void Start()
    {
        _infoPanel = ItemInfoUIManager.Instance;
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
        _infoPanel.SetPosition(transform.position + Vector3.up * .35f);
        _infoPanel.SetSize(_infoPanelSize.x, _infoPanelSize.y);
        _infoPanel.Show();
        _infoPanel.SetTitleAndDescription("Platform Switch", "Hit Use button [E] when touching the switch to switch platform on / off.");
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