using Cinemachine;
using DG.Tweening;
using StarterAssets;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class WalkieTalkie : MonoBehaviour
{
    public GameObject[] zones;
    public GameObject zoneImage;
    private StarterAssetsInputs cam;
    private FirstPersonController controller;

    private bool isOpen;

    private void Start()
    {
        cam = FindAnyObjectByType<StarterAssetsInputs>();
        controller = FindAnyObjectByType<StarterAssets.FirstPersonController>();
        foreach (var zone in zones)
        {
            zone.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            foreach (var zone in zones)
            {
                if (zone.activeSelf)
                {
                    zone.SetActive(false);
                }
                else
                {
                    zone.SetActive(true);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleZone();
        }
    }

    void ToggleZone()
    {
        isOpen = !isOpen;

        if (isOpen)
        {
            zoneImage.SetActive(true);

            zoneImage.transform.DOScaleY(1f, 0.3f).SetEase(Ease.OutBack);

            Cursor.lockState = CursorLockMode.None;
            cam.lockLook = true;

            controller.lockMove = true;
            controller.lockLook = true;
        }
        else
        {
            zoneImage.transform.DOScaleY(0f, 0.25f)
                .SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    zoneImage.SetActive(false);
                });

            Cursor.lockState = CursorLockMode.Locked;
            cam.lockLook = false;

            controller.lockMove = false;
            controller.lockLook = false;
        }
    }

    private void OnDisable()
    {
        if(zoneImage == false) return;
        zoneImage.transform.DOScaleY(0f, 0.25f)
        .SetEase(Ease.InBack)
        .OnComplete(() =>
        {
            zoneImage.SetActive(false);
        });

        Cursor.lockState = CursorLockMode.Locked;
        cam.lockLook = false;

        controller.lockMove = false;
        controller.lockLook = false;

        foreach (var zone in zones)
        {
            zone.SetActive(false);
        }
    }
}
