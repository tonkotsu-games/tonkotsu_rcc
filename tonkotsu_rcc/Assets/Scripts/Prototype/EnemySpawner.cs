using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float spawnTickFrequency;
    [SerializeField] AnimationCurve spawnChanceOverTimePerTick;
    [SerializeField] bool on = true;
    [SerializeField] GameObject enemyPrefab;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (on)
        {
            yield return new WaitForSeconds(spawnTickFrequency);

            bool spawn = Random.value < spawnChanceOverTimePerTick.Evaluate(Time.time);

            if (spawn)
            {
                Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            }
        }
    }

}
