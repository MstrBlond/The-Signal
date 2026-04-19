using System.Collections.Generic;
using UnityEngine;

public class AlertZone : MonoBehaviour
{
    public char zoneLetter;
    public int dangerQuantity = 0;

    private HashSet<IDanger> detected = new HashSet<IDanger>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDanger>(out var danger))
        {
            if (!detected.Contains(danger))
            {
                detected.Add(danger);
                dangerQuantity++;
                Debug.Log("Entered " + other.name);
            }
        }
    }

    public void ClearDangers()
    {
        foreach (var danger in detected)
        {
            if (danger is MonoBehaviour mb && mb != null)
            {
                var particles = mb.GetComponentsInChildren<ParticleSystem>();

                if (particles.Length > 0)
                {
                    foreach (var ps in particles)
                    {
                        ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                    }
                }

                Destroy(mb.gameObject, 20f);
            }
        }

        detected.Clear();
        dangerQuantity = 0;
    }
}