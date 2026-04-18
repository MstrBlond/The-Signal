using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTowerController : MonoBehaviour, ITowerController
{
    public int power { get; private set; }

    public float range { get; private set; }

    public void Attack()
    {
        Debug.Log("Main Tower Attacking with power: " + power + " and range: " + range);
    }
}