using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class env : MonoBehaviour
{

    public Scrollbar sb;

    private void Start()
    {

    }

    public void timeChange()
    {
        Time.timeScale = sb.value * 10;
    }
}
