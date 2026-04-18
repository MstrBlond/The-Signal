using System.Collections.Generic;
using UnityEngine;

public class TowerNode : MonoBehaviour
{
    public List<TowerNode> connectedTowers = new List<TowerNode>();

    public bool hasPower = false;
    public bool isGenerator = false;

    public void Connect(TowerNode other)
    {
        if (other == this) return;

        if (!connectedTowers.Contains(other))
        {
            connectedTowers.Add(other);
            other.connectedTowers.Add(this);
        }
    }

    public void Disconnect(TowerNode other)
    {
        connectedTowers.Remove(other);
        other.connectedTowers.Remove(this);
    }
}