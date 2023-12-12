using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacteriaTeam : MonoBehaviour
{
    public Color col;

    public int TeamID;

    [SerializeField] private GameObject bacteria;

    private void Start()
    {
        BacteriaController bact = Instantiate(bacteria, new Vector2(Random.Range(-990, 990), Random.Range(-490, 490)), Quaternion.identity, this.transform).GetComponent<BacteriaController>();
        
        bact.TeamID = this.TeamID;

        col = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        bact.GetComponent<SpriteRenderer>().color = col;
    }
}
