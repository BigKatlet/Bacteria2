using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject enemy;
    [SerializeField] private int maxEnemies;
    [SerializeField] private float delay;

    public GameObject enemyParent;

    IEnumerator spawnEnemy()
    {
        yield return new WaitForSeconds(delay);

        int ttlEnemies = GameObject.FindGameObjectsWithTag("enemy").Length;
        if (ttlEnemies < maxEnemies)
        {
            Vector3 pos = new Vector3(Random.Range(-250, 250), Random.Range(-250, 250), 0);
            Instantiate(enemy, pos, Quaternion.identity, enemyParent.transform);
        }
        StartCoroutine(spawnEnemy());
    }

    private void Start()
    {
        StartCoroutine(spawnEnemy());
    }

}
