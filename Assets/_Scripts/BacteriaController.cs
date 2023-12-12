using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BacteriaController : MonoBehaviour
{


    [SerializeField] public int TeamID;

    [SerializeField] private Vector3 targetPosition;

    [SerializeField] private float speed;

    [SerializeField] private int foodAmount;

    [SerializeField] private BacteriaTeam team;

    [SerializeField] private GameObject bacteria;

    public GameObject findNearestObjectWithTag(string tag)
    {
        List<GameObject> objects = GameObject.FindGameObjectsWithTag(tag).ToList();
        if (this.gameObject.tag == tag) { objects.Remove(this.gameObject); }

        if (objects.Count > 0)
        {
            GameObject curObject = objects[0];
            foreach (GameObject obj in objects)
            {
                float curDistance = Vector2.Distance(this.gameObject.transform.position, obj.transform.position);
                float oldDistance = Vector2.Distance(curObject.transform.position, this.transform.position);

                if (curDistance < oldDistance)
                {
                    curObject = obj;
                }
            }
            return curObject;
        }
        else
        {
            return null;
            print("[ERR] There is no other objects with tag " + tag);
        }
    }

    private void Start()
    {
        StartCoroutine(findTarget());
        StartCoroutine(foodDecreasing());
        foodAmount = 10;
        targetPosition = this.transform.position;
        this.gameObject.name = "Bacteria";
    }

    private void Update()
    {
        if (targetPosition != null && targetPosition != this.transform.position)
        {
            transform.position += (targetPosition - transform.position).normalized * speed * Time.deltaTime;
        }
    }

    private IEnumerator findTarget()
    {
        yield return new WaitForSeconds(0.55f);
        if (GameObject.FindGameObjectsWithTag("food").Length > 0)
        {
            targetPosition = findNearestObjectWithTag("food").transform.position;
        }
        StartCoroutine(findTarget());
    }

    private IEnumerator foodDecreasing()
    {
        yield return new WaitForSeconds(1f);
        foodAmount--;
        if(foodAmount <= 0)
        {
            Destroy(this.gameObject);
        }
        StartCoroutine(foodDecreasing());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
            case "food":
                Destroy(collision.gameObject);
                foodAmount += 5;
                targetPosition = this.transform.position;
                if (foodAmount >= 30)
                {
                    Instantiate(bacteria, new Vector3(this.transform.position.x, this.transform.position.y - 3), Quaternion.identity);
                    foodAmount -= 20;
                }
                break;
            case "bacteria":
                if (collision.gameObject.GetComponent<BacteriaController>().TeamID != this.TeamID)
                {
                    if (this.foodAmount >= collision.gameObject.GetComponent<BacteriaController>().foodAmount)
                    {
                        Destroy(collision.gameObject);
                    }
                }
                break;
        }
    }
}
