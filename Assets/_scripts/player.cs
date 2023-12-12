using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{

    [SerializeField] private float spd;
    [SerializeField] private Text pointText;
    public static int points;
    private Camera cam;
    //private bool WASD_Switch;
    private Transform mouseTarget;

    [SerializeField] private bool WASD;

    void Start()
    {
        points = 200;
        cam = GameObject.FindGameObjectWithTag("cam").GetComponent<Camera>();
        TakeScore(0);
    }

    void FixedUpdate()
    {
            if (this.gameObject != null)
            {
                float x, y;
                Vector2 mov;

                x = Input.GetAxis("Horizontal"); y = Input.GetAxis("Vertical");
                mov = new Vector3(x * spd, y * spd);
                this.gameObject.transform.transform.Translate(mov);
            } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "shit")
        {
            Destroy(collision.gameObject);
            TakeScore(10);
        }
    }
    public void TakeScore(int value)
    {
        //Add score
        points += value;
        //Set scale
        this.transform.localScale = new Vector2(points * 0.001f, points * 0.001f);
        //Cam zoom
        float plrScl = this.transform.localScale.x; float camZoom = plrScl * cam.GetComponent<cam>().zoomPow;
        cam.GetComponent<cam>().Zoom(camZoom);
        //Update text
        pointText.text = points.ToString();
    }
}
