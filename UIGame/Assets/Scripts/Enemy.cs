using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameObject player;

    public Transform[] waypoints;

    Transform currentWaypoint;

    Rigidbody2D enemyRB;

    float speed = 5f;

    bool detectMode = false;
    Transform target;

    float detectTime = 20f;

    float move = 4;
	// Use this for initialization
	void Start () {

        transform.position = waypoints[0].position;
        currentWaypoint = waypoints[0];
		
	}
	
	// Update is called once per frame
	void Update () {

        if (target  == null && !detectMode)
        {
            Walk();
        }

        else if (target != null && !player.GetComponent<Player>().IsHiding() && !detectMode)
        {

            float distance = Vector2.Distance(transform.position, target.position);
            float movementDistance = move * Time.deltaTime;
            transform.position =  Vector2.MoveTowards(transform.position, target.position, movementDistance);
        }

        else if (detectMode || player.GetComponent<Player>().IsHiding())
        {
            DetectMode();
        }
		
	}

//TODO Use raycasts instead of collision for player detection
    private void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.gameObject.tag == "Player" && !player.GetComponent<Player>().IsHiding())
        {
            detectMode = false;
            target = player.transform;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !player.GetComponent<Player>().IsHiding())
        {
            detectMode = false;
            target = player.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            target = null;
            detectMode = true;
        }
    }


    //TODO will be used for when enemy loses player, but it still searching for player
    private void DetectMode()
    {
        Vector3 relative = transform.InverseTransformPoint(player.transform.position);

        detectTime -= Time.deltaTime;

        if (relative.x < 0.0 && detectTime > 0)
        {
            transform.position -= new Vector3(0.05f, 0);
        }
        else if (relative.x > 0.0 && detectTime > 0)
        {
            transform.position += new Vector3(0.05f, 0);
        }
        if (detectTime < 0)
        {
            Debug.Log("Detect Mode done");
            detectMode = false;
        }
    }

    private void Walk()
    {
        if (currentWaypoint == waypoints[0])
        {

            transform.position = Vector2.MoveTowards(transform.position, waypoints[1].position, speed * Time.deltaTime);

          if (transform.position.x >= waypoints[1].position.x)
            {
                currentWaypoint = waypoints[1];
                
            }
        }

        if (currentWaypoint == waypoints[1])
        {

            transform.position = Vector2.MoveTowards(transform.position, waypoints[0].position, speed * Time.deltaTime);

            if (transform.position.x <= waypoints[0].position.x)
            {
                currentWaypoint = waypoints[0];
            }
        }
    }

    private void Stop(float time)
    {
        time -= Time.deltaTime;
        speed = 0;

        if (time < 0f)
        {
            speed = 5;
            return;
        }
    }
}
