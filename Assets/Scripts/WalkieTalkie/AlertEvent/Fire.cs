using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour, IDanger
{
    private void Start()
    {
        GetComponent<Collider>().enabled = false;
        Activate();
    }

    public void Activate()
    {
        GetComponent<Collider>().enabled = true;
    }
}
