using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class God : MonoBehaviour, IDanger
{
    public GameObject _obj;

    private void Start()
    {
        GetComponent<Collider>().enabled = false;
    }

    public void Activate()
    {
        GetComponent<Collider>().enabled = true;
        _obj.SetActive(true);
    }
}
