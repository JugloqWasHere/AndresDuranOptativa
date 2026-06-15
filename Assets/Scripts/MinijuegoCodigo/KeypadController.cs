using System.Collections;
using TMPro;
using UnityEngine;

public class KeypadController : MonoBehaviour
{
    [Header("Código")]
    public string correctCode = "1234";

    [Header("Display")]
    public TMP_Text displayText;

    [Header("Objetos Gameplay")]
    public GameObject player;
    public GameObject cameraController;
    public GameObject thirdPersonUI;

    [Header("Cámara Keypad")]
    public Camera keypadCamera;

    [Header("Puerta")]
    public Door targetDoor;

    private string currentCode = "";
    private bool usingKeypad;

    private void Start()
    {
        keypadCamera.gameObject.SetActive(false);

        ClearDisplay();

     
    }

    public void EnterKeypad()
    {
        if (usingKeypad)
            return;

        usingKeypad = true;

        player.SetActive(false);

        cameraController.SetActive(false);

        thirdPersonUI.SetActive(false);

        keypadCamera.gameObject.SetActive(true);

        Cursor.lockState = CursorLockMode.None;

        currentCode = "";

        ClearDisplay();
    }

    public void ExitKeypad()
    {
        usingKeypad = false;

        keypadCamera.gameObject.SetActive(false);

        player.SetActive(true);

        cameraController.SetActive(true);

        thirdPersonUI.SetActive(true);

        currentCode = "";

        ClearDisplay();

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void PressNumber(string number)
    {
        if (!usingKeypad)
            return;

        if (currentCode.Length >= 4)
            return;

        currentCode += number;

        displayText.text = currentCode;
    }

    public void PressCancel()
    {
        ExitKeypad();
    }

    public void PressAccept()
    {
        if (!usingKeypad)
            return;

        StartCoroutine(CheckCode());
    }

    IEnumerator CheckCode()
    {
        if (currentCode == correctCode)
        {
            displayText.text = "OK";

            yield return new WaitForSeconds(0.5f);

            targetDoor.isOpen = true;

            ExitKeypad();
        }
        else
        {
            displayText.text = "ERROR";

            yield return new WaitForSeconds(1f);

            currentCode = "";

            ClearDisplay();
        }
    }

    void ClearDisplay()
    {
        displayText.text = "0000";
    }
}