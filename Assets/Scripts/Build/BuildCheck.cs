using UnityEngine;

public class BuildCheck : MonoBehaviour
{
    public bool isBuilding = false;
    public bool canBuild = false;

    public int boxCollidersInside = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            boxCollidersInside++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            boxCollidersInside--;
        }
    }

    private void Update()
    {
        if (isBuilding)
        {
            canBuild = boxCollidersInside >= 2;
            Debug.Log("Box colliders inside: " + boxCollidersInside);

            if (canBuild)
                Debug.Log("Can build here!");
            else
                Debug.Log("Cannot build here!");
        }
    }
}
