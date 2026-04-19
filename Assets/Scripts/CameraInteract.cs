using UnityEngine;

public class CameraInteract : MonoBehaviour
{
    public TextController textController;

    private IUse currentUse;
    private bool isInTrigger;

    void Update()
    {
        if (currentUse == null) return;

        if (currentUse.isOpen)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                currentUse.Use();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                textController.HideTextInstantly();
                currentUse.Use();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<IUse>(out var use)) return;

        currentUse = use;
        isInTrigger = true;

        if (textController != null)
        {
            textController.ShowText(use.ShowText());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<IUse>(out var use)) return;

        if (use != currentUse) return;

        currentUse = null;
        isInTrigger = false;

        if (textController != null)
        {
            textController.HideTextInstantly();
        }
    }
}