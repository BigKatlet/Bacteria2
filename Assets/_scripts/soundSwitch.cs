using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundSwitch : MonoBehaviour
{
    private static bool active;
    private GameObject player;
    public GameObject musOn, musOff;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(active == null)
        {
            active = true;
        }
        musicOn();
    }

    private void musicOn()
    {
        musOn.SetActive(true);
        musOff.SetActive(false);
        player.GetComponent<AudioSource>().volume = 1;
    }
    private void musicOff()
    {
        musOff.SetActive(true);
        musOn.SetActive(false);
        player.GetComponent<AudioSource>().volume = 0;
    }

    public void switchState()
    {
        active = !active;
        if(active == false)
        {
            musicOff();
        }
        else if(active == true)
        {
            musicOn();
        }
    }
}
