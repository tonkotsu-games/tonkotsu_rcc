using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float spawnTickFrequency;
    [SerializeField] AnimationCurve spawnChanceOverTimePerTick;
    [SerializeField] bool on = true;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float range = 20;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {

            yield return new WaitForSeconds(spawnTickFrequency);

            if (!on)
            {
                continue;
            }

            if(Vector3.Distance(PlayerHandler.Player.position, transform.position) > range)
            {
                continue;
            }

            bool spawn = Random.value < spawnChanceOverTimePerTick.Evaluate(Time.time);

            if (spawn)
            {
                Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, range);
    }
}
