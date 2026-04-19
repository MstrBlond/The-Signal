using UnityEngine;

public class DangerController : MonoBehaviour
{
    public AlertZone[] alertZones;

    public void ChooseZone(string letter)
    {
        if (string.IsNullOrEmpty(letter)) return;

        char input = char.ToUpper(letter[0]);

        foreach (var zone in alertZones)
        {
            if (char.ToUpper(zone.zoneLetter) == input)
            {
                zone.ClearDangers();
                return;
            }
        }

    }
}
