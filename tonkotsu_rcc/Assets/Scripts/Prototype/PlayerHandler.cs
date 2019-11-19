using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : Singleton<PlayerHandler>
{
    [SerializeField] bool spawnPlayer;
    [SerializeField] GameObject playerPrefab;

    PlayerController player;

    public static Transform Player { get => Instance.GetPlayerTransform(); }

    protected override void Awake()
    {
        base.Awake();

        if (spawnPlayer)
        {
            var go = Instantiate(playerPrefab);

            player = go.GetComponent<PlayerController>();
        }
        else
        {
            player = GameObject.FindObjectOfType<PlayerController>();
        }

        if (player == null)
        {
            Debug.LogError("No PlayerController found");
        }
        else
        {
            var spawnPos = GameObject.FindObjectOfType<PlayerSpawnPos>();

            if (spawnPos != null)
                player.transform.position = spawnPos.transform.position;
        }
    }


    private Transform GetPlayerTransform()
    {
        if(Instance.player != null)
        {
            return Instance.player.transform;
        }
        else
        {
            Debug.LogWarning("Creating Empty Player to avoid NullReferences");
            var go = new GameObject("__ERROR_AVOIDING_TEMP_PLAYER");
            go.hideFlags = HideFlags.DontSave;
            Instance.player = go.AddComponent<PlayerController>();

            return go.transform;
        }
    }

}
