using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPos : MonoBehaviour, IHierarchyEffector
{
    public HierarchyDisplayElement GetHierarchyEffect()
    {
        return new HierarchyDisplayElement("Spawn");
    }
}
