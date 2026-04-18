using System.Collections.Generic;
using UnityEngine;

public static class PowerSystem
{
    public static void UpdateElectricity()
    {
        var allTowers = GameObject.FindObjectsOfType<TowerNode>();

        foreach (var t in allTowers)
            t.hasPower = false;

        foreach (var generator in allTowers)
        {
            if (!generator.isGenerator) continue;

            SpreadPower(generator);
        }
    }

    private static void SpreadPower(TowerNode start)
    {
        Queue<TowerNode> queue = new Queue<TowerNode>();

        start.hasPower = true;
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            foreach (var next in current.connectedTowers)
            {
                if (!next.hasPower)
                {
                    next.hasPower = true;
                    queue.Enqueue(next);
                }
            }
        }
    }
}