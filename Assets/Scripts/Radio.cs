using Cinemachine;
using DG.Tweening;
using StarterAssets;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;

public class Radio : MonoBehaviour, IUse
{
    public int signalStrength = 0;
    public int signalDelay;
    public GameObject signalCanvas;
    public GameObject contentToHide;
    public Transform focusPoint;
    public CinemachineVirtualCamera signalCamera;
    
    public SignalInput signalInput;

    public AlertController alertController;

    public TextController textController;

    private StarterAssetsInputs cam;
    private FirstPersonController controller;

    public bool isDecoded = false;

    public bool isOpen { get; set; }


    private void Start()
    {
        cam = FindAnyObjectByType<StarterAssetsInputs>();
        controller = FindAnyObjectByType<StarterAssets.FirstPersonController>();
        Invoke("ReceiveSignal", signalDelay);
    }

    public void ReceiveSignal()
    {
        signalStrength++;
        isDecoded = true;
        Debug.Log("Received signal. Current strength: " + signalStrength);

        alertController.PlayAlert();
    }

    public void Use()
    {
        isOpen = !isOpen;

        if (isOpen)
        {
            signalCanvas.SetActive(true);

            signalCanvas.transform.DOScaleY(1f, 0.3f).SetEase(Ease.OutBack);

            Cursor.lockState = CursorLockMode.None;
            cam.lockLook = true;

            signalCamera.Priority = 100;

            controller.lockMove = true;
            controller.lockLook = true;

            contentToHide.SetActive(false);

            if (signalStrength == 1)
            {
                signalInput.StartInput();
            }
            textController.HideCheersInstantly();
        }
        else
        {
            signalCanvas.transform.DOScaleY(0f, 0.25f)
                .SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    signalCanvas.SetActive(false);
                });

            Cursor.lockState = CursorLockMode.Locked;
            cam.lockLook = false;

            signalCamera.Priority = 0;

            controller.lockMove = false;
            controller.lockLook = false;

            contentToHide.SetActive(true);

            signalInput.StopInput();

            Invoke("Decoded", 1f);
        }
    }

    public string ShowText()
    {
        return isDecoded ? "Decode Signal" : "No Signal To Decode";
    }

    public void Decoded()
    {
        textController.ShowCheersText(
            $"Signal Decoded By {(float)signalStrength/4*100}%\r\nWait For Future Signals."
            );
    }
}
