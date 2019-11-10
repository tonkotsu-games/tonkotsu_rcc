using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PrefabInstantiator
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void RuntimeInit()
    {
        Debug.Log("Instantiating AutoPrefabs");
        InstantiateAutoPrefabs();
    }

    private static void InstantiateAutoPrefabs()
    {
        var gos = Resources.LoadAll<GameObject>("AutoPrefabs/");

        foreach (var prefab in gos)
        {
            var g = MonoBehaviour.Instantiate(prefab);
            g.name = "_AUTO_" + prefab.name;
            g.hideFlags = HideFlags.DontSaveInBuild | HideFlags.DontSaveInEditor;
        }
    }
}
