using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shitSpawner : MonoBehaviour
{


    [SerializeField] private GameObject shitObject;
    [SerializeField] private GameObject shitParent;
    [SerializeField] private int maxShits;

    void newShit()
    {
        float scaleRan = Random.Range(0.03f, 0.05f); Vector3 scale = new Vector3(scaleRan, scaleRan, 0);
        Vector3 pos = new Vector3(Random.Range(-250, 250), Random.Range(-250, 250), 0);
        GameObject nShit = Instantiate(shitObject, pos, Quaternion.identity, shitParent.transform) as GameObject;
        nShit.transform.localScale = scale;
    }

    void Start()
    {
        newShit();
    }

    void Update()
    {
        int ttlShits = GameObject.FindGameObjectsWithTag("shit").Length;
        if(ttlShits <= maxShits)
        {
            newShit();
        }
    }
}
