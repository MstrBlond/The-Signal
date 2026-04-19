using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DangerActivator : MonoBehaviour
{
    public List<IDanger> dangers = new List<IDanger>();
    public float firstDelay;
    public float secondDelay;

    public void Start()
    {
        dangers = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDanger>()
            .ToList();

        StartCoroutine(Activate());
    }

    IEnumerator Activate()
    {
        while (dangers.Count > 0)
        {
            yield return new WaitForSeconds(5f);
            int dangerNumber = Random.Range(0, dangers.Count);

            dangers[dangerNumber].Activate();
            dangers.RemoveAt(dangerNumber);

            yield return new WaitForSeconds(Random.Range(firstDelay, secondDelay));
        }
    }
}