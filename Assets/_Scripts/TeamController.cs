using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamController : MonoBehaviour
{
    [SerializeField] public BacteriaTeam[] teams;

    [SerializeField] private BacteriaTeam teamControllerSample;

    private void Start()
    {
        for(int x = 0; x < teams.Length; x++)
        {
            BacteriaTeam curTeam = Instantiate(teamControllerSample, GameObject.Find("env").transform);
            teams[x] = curTeam;
            curTeam.TeamID = x;
        }
    }
}
