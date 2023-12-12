using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyAI : MonoBehaviour
{
    //movement
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    //Player
    private GameObject playerObj;
    float distance;
    //Hunter
    [SerializeField] public bool isHunter;
    //Enemies
    [SerializeField] private float maxEnemyDistance;
    private GameObject[] enemies;
    private GameObject nearestEnemy;
    [SerializeField] float distanceToEnemy;
    //Meal
    private GameObject[] shits;
    private GameObject clsstShit;
    //Points
    [SerializeField] private int points;
    [SerializeField] private Text text;

    GameObject FindClosestShit()
    {
        shits = GameObject.FindGameObjectsWithTag("shit");
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in shits)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                clsstShit = go;
                distance = curDistance;
            }
        }
        return clsstShit;
    }

    GameObject FindClosestEnemy()
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in enemies)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if(go == this.gameObject)
            {
                continue;
            }
            if (curDistance < distance)
            {
                nearestEnemy = go;
                distance = curDistance;
            }
        }
        return nearestEnemy;
    }

    //Eating
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Eating meal
        if(collision.gameObject.tag == "shit")
        {
            //Destroying, adding points
            Destroy(collision.gameObject);
            this.TakeScore(10);
        }
    }

    //Fighting
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Player fight
        if (collision.gameObject.tag == "Player")
        {
            //Player wins
            if (player.points > points)
            {
                playerObj.GetComponent<player>().TakeScore(Mathf.RoundToInt(this.points / 4));
                Destroy(this.gameObject);
            }
            //Enemy wins
            else if(player.points < points)
            {
                Destroy(collision.gameObject);
                this.TakeScore(Mathf.RoundToInt(player.points / 4));
            }
        }
        //Enemy fight
        if(collision.gameObject.tag == "enemy")
        {
            //This wins
            if (this.points > collision.gameObject.GetComponent<enemyAI>().points)
            {
                this.TakeScore(Mathf.RoundToInt(collision.gameObject.GetComponent<enemyAI>().points / 4));
                Destroy(collision.gameObject);
            }
            //Other wins
            else if (this.points < collision.gameObject.GetComponent<enemyAI>().points)
            {
                collision.gameObject.GetComponent<enemyAI>().TakeScore(Mathf.RoundToInt(this.points / 4));
                Destroy(this.gameObject);
            }
        }
    }

    public void TakeScore(int value)
    {
        //Scale
        this.transform.localScale = new Vector3(points * 0.005f, points * 0.005f, 0);
        //Max enemy distance
        maxEnemyDistance = this.transform.localScale.x * 25;
        //Adding score
        this.points += value;
        //Updating text
        text.text = points.ToString();
    }

    void Start()
    {
        //Hunter or not
        //Not hunter
        int huntInt = Random.Range(0, 2);
        if (huntInt == 0)
        {
            isHunter = false;
        }
        //Hunter
        else
        {
            isHunter = true;
        }
        //points
        points = 200;
        //Upd scale
        TakeScore(0);
        //Player
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        //Distance to player
        if (playerObj != null)
        {
            distance = Vector3.Distance(this.gameObject.transform.position, playerObj.transform.position);
        }
        //Distance to enemy
        if (FindClosestEnemy() != null)
        {
            distanceToEnemy = Vector3.Distance(FindClosestEnemy().transform.position, this.gameObject.transform.position);
        }
        //Setting player as target
        if (distance < maxEnemyDistance && player.points < points && GameObject.FindGameObjectWithTag("Player") != null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        //Target is not a player
        else
        {
            //Hunter
            if(isHunter == true)
            {
                //Found closest enemy
                if (FindClosestEnemy() != null)
                {
                    //Setting closest enemy as target
                    if (distanceToEnemy < maxEnemyDistance && this.points > FindClosestEnemy().GetComponent<enemyAI>().points)
                    {
                        target = FindClosestEnemy().transform;
                    }
                    //Setting meal as target
                    else
                    {
                        target = FindClosestShit().transform;
                    }
                }
            }
            //Farmer
            else
            {
                target = FindClosestShit().transform;
            }
        }
        //Movement
        if (target != null)
        {
            transform.position += (target.position - transform.position).normalized * speed * Time.deltaTime;
        }
    }

}
