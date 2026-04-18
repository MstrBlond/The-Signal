using UnityEngine;

public class MainTowerController : MonoBehaviour, ITowerController
{
    [field: SerializeField] public int Power { get; private set; }

    [field: SerializeField] public float Range { get; private set; }

    public void Attack()
    {
        Debug.Log($"Main Tower Attacking with power: {Power} and range: {Range}");
    }
}