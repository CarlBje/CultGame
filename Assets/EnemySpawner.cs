using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] spawnPoints;
    public GameObject[] waypointParents;

    public float spawnInterval = 5f;
    public float turnTime = 10f;

    public int maxEnemies = 5;
    public int maxTurns = 5;
    private int currentTurn = 0;
    private int currentSpawn = 0;

    private Coroutine spawnCoroutine;

    private void Start()
    {
        spawnCoroutine = StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (currentTurn < maxTurns)
        {
            for (int i = 0; i < maxEnemies; i++)
            {
                if (currentSpawn >= enemyPrefabs.Length)
                {
                    Debug.Log("Resetting currentSpawn to 0");
                    currentSpawn = 0;
                }

                int spawnIndex = Random.Range(0, spawnPoints.Length);
                Debug.Log("Spawning enemy " + (i + 1) + " of turn " + (currentTurn + 1) + " at spawn point " + (spawnIndex + 1));

                GameObject enemy = Instantiate(enemyPrefabs[currentSpawn], spawnPoints[spawnIndex].transform.position, Quaternion.identity);
                EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();

                if (enemyMovement != null && spawnIndex < waypointParents.Length)
                {
                    Debug.Log("Setting waypointParent for enemy " + (i + 1));
                    enemyMovement.SetWaypointParent(waypointParents[spawnIndex].transform);
                    Debug.Log("Enemy waypoint parent: " + enemyMovement.waypointParent);
                    //Debug log listing all the child waypoints
                    for (int j = 0; j < enemyMovement.waypointParent.childCount; j++)
                    {
                        Debug.Log("Waypoint " + (j + 1) + ": " + enemyMovement.waypointParent.GetChild(j).position);
                    }
                }

                currentSpawn++;
                yield return new WaitForSeconds(spawnInterval);
            }

            currentTurn++;
            yield return new WaitForSeconds(turnTime);
        }
    }
}
