using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject foodSample;
    [SerializeField] private GameObject bactSample;

    [SerializeField] private float foodSpawnSpeed;
    [SerializeField] private float bactSpawnSpeed;

    [SerializeField] private float[] X_size;
    [SerializeField] private float[] Y_size;

    private void Start()
    {
        StartCoroutine(spawnFood());
    }

    private IEnumerator spawnFood()
    {
        yield return new WaitForSeconds(foodSpawnSpeed);

        if(GameObject.FindGameObjectsWithTag("food").Length < 300)
        {
            Instantiate(foodSample, new Vector2(Random.Range(X_size[0], X_size[1]), Random.Range(Y_size[0], Y_size[1])), Quaternion.identity, GameObject.Find("Foods").transform);
        }

        StartCoroutine(spawnFood());
    }
}
