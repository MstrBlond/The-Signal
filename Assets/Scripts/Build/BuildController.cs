using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : MonoBehaviour
{
    public GameObject towerPrefab;
    private bool isBuilding = false;
    private GameObject currentTower;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                Vector3 worldPos = hit.point;
                if(!isBuilding)
                {
                    currentTower = Instantiate(towerPrefab, worldPos, Quaternion.identity);
                    currentTower.GetComponent<BuildCheck>().isBuilding = true;
                }
                isBuilding = true;
                currentTower.transform.position = worldPos;
            }

        }
        if (Input.GetMouseButtonUp(0) && isBuilding)
        {
            isBuilding = false;
            currentTower.GetComponent<BuildCheck>().isBuilding = false;

            if(!currentTower.GetComponent<BuildCheck>().canBuild)
            {
                Destroy(currentTower);
            }

            currentTower = null;
        }

        PowerSystem.UpdateElectricity();

    }
}
