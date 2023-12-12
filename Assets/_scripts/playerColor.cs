using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerColor : MonoBehaviour
{
    void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}
