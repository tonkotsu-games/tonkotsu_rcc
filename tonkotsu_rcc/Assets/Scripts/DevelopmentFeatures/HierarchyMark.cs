using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class HierarchyMark : MonoBehaviour, IHierarchyEffector
{
    [Label("Write Text?")]
    [HideIf("useTexture")]
    [SerializeField] bool useText;

    [Label("Use Texture?")]
    [HideIf("useText")]
    [SerializeField] bool useTexture;
    
    [ShowIf("useTexture")]
    [SerializeField] Texture texture;

    [ShowIf("useText")]
    [SerializeField] string content;

    [HideIf(ConditionOperator.Or, "useText", "useTexture")]
    [SerializeField] Color color;

    public HierarchyDisplayElement GetHierarchyEffect()
    {
        if (useText)
        {
            return new HierarchyDisplayElement(content);
        }
        else if (useTexture)
        {
            return new HierarchyDisplayElement(texture);
        }
        else
        {
            return new HierarchyDisplayElement(color);
        }
    }
}
