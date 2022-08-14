using UnityEngine;

public class SwithController : MonoBehaviour
{
    [SerializeField] GameObject _switchable;

    private ISwitchable _switchableScript;
    private bool _isInTriggerZone;

    private void Awake()
    {
        _switchableScript = _switchable.GetComponent<ISwitchable>();
        if (_switchableScript == null)
        {
            Debug.LogError("Switchable game object must implement ISwitchable interface.");
            return;
        }
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
}