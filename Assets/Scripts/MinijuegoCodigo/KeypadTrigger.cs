using UnityEngine;

public class KeypadTrigger : MonoBehaviour
{
    public KeypadController keypad;
    public GameObject Tooltip;

    private bool playerInside;

    private void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            keypad.EnterKeypad();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Tooltip.SetActive(true);
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Tooltip.SetActive(false);
            playerInside = false;
        }
    }
}