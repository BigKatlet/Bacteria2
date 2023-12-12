using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cam : MonoBehaviour
{

    [SerializeField] public float zoomPow;
    private GameObject playerObj;
    Vector3 playerPos;
    public static Transform mouseTgt;

    void Start()
    {
        //Setting correct game speed
        Time.timeScale = 1;
        //Finds player
        playerObj = GameObject.FindGameObjectWithTag("Player");
        //Need to correct working of camera
        playerPos.z = -10;
    }

    public float translationFactor = 20;

    void FixedUpdate()
    {
        //Moving
        if (playerObj != null)
        {
            if (transform.position != playerObj.transform.position)
            {
                playerPos.x = playerObj.transform.position.x;
                playerPos.y = playerObj.transform.position.y;
                transform.position += (playerPos - transform.position) / translationFactor;
            }
        }
    }

    public void Zoom(float camZoom)
    {
        this.gameObject.GetComponent<Camera>().orthographicSize = camZoom;
    }

    /*public Transform getMousePos()
{
    //Mouse pos
    Vector2 mouse = Input.mousePosition;
    Ray castPoint = Camera.main.ScreenPointToRay(mouse);
    RaycastHit hit;
    if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
    {
        mouse = hit.transform.position;
    }

    return mouseTgt;
}*/
}
