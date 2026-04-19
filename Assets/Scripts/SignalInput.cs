using UnityEngine;
using TMPro;

public class SignalInput : MonoBehaviour
{
    public TMP_Text letterText;
    public int maxInputs = 7;

    private int inputCount = 0;
    private string currentLetter;
    private bool isActive = false;

    public void StartInput()
    {
        inputCount = 0;
        isActive = true;
        GenerateLetter();
    }

    public void StopInput()
    {
        isActive = false;
    }

    void Update()
    {
        if (!isActive) return;

        for (KeyCode key = KeyCode.A; key <= KeyCode.Z; key++)
        {
            if (Input.GetKeyDown(key))
            {
                char pressed = key.ToString()[0];

                if (pressed.ToString() == currentLetter)
                {
                    inputCount++;
                    Debug.Log("Correct: " + inputCount);

                    if (inputCount >= maxInputs)
                    {
                        Debug.Log("Signal decoded!");
                        isActive = false;
                        GetComponent<Radio>().isDecoded = false;
                        GetComponent<Radio>().Use();
                        return;
                    }

                    GenerateLetter();
                }
            }
        }
    }

    void GenerateLetter()
    {
        char letter = (char)Random.Range(65, 91); // A-Z
        currentLetter = letter.ToString();
        letterText.text = currentLetter;
    }
}