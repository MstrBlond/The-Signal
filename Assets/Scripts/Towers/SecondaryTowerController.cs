using UnityEngine;

public class SecondaryTowerController : MonoBehaviour
{
    private TowerNode node;

    private void Awake()
    {
        node = GetComponent<TowerNode>();
    }

    private void Update()
    {

    }
}