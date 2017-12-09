using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class Enemy : MonoBehaviour {

    public GameObject player;

    public Transform[] waypoints;

    Image terrorBar;

    Transform currentWaypoint;

    Rigidbody2D enemyRB;

    float speed = 5f;

    bool detectMode = false;
    Transform target;

    float detectTime = 20f;

    float move = 4;

    bool playerFound = false;

    float whichDirection = 0;

    // Event handling
    SeeEnemyEvent seeEnemyEvent;

	// Use this for initialization
	void Start () {
        seeEnemyEvent = new SeeEnemyEvent();
        EventManager.EnemyInvoker(this);
        transform.position = waypoints[0].position;
        currentWaypoint = waypoints[0];
        target = null;

        try
        {
            terrorBar = GameObject.Find("TerrorBar").GetComponent<Image>();
        }
        catch (NullReferenceException e)
        {
            Debug.Log("Couldn't find terrorBar, moving on");
        }
		
	}
	
	// Update is called once per frame
	void Update () {

        if (target  == null && !detectMode)
        {
            Walk();
        }

        else if (target != null  && playerFound)
        {
            float origx = transform.position.x;
            float distance = Vector2.Distance(transform.position, target.position);
            float movementDistance = move * Time.deltaTime;
            transform.position =  Vector2.MoveTowards(transform.position, target.position, movementDistance);
            float newx = transform.position.x;
            whichDirection = newx - origx;
            terrorBar.fillAmount -= 0.001f;

            // Lost player, go searching
            if (distance > 15f)
            {
                detectTime = 20f;
                playerFound = false;
                detectMode = true;

            }
        }

        else if (detectMode && !playerFound)
        {
            DetectMode();
        }
		
	}

    private void FixedUpdate()
    {
        RayCastPlayer();
    }
    
    /// <summary>
    /// Use raycasting to see if player is in enemy's sight
    /// </summary>
    void RayCastPlayer()
    {
       
        Vector2 rayDirection = player.transform.position - transform.position;

        float distance = 12f;
        Debug.DrawRay(transform.position, rayDirection * distance, Color.red, 2f, false);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, distance, LayerMask.GetMask("Player"));

        if (hit && !player.GetComponent<Player>().IsHiding())
        { 
            
            target = player.transform;
            playerFound = true;

            
        }

        if (hit && player.GetComponent<Player>().IsHiding())
        {
           
        }
    }

   /// <summary>
   /// If the player has been detected, have the enemy investigate by walking in the same direction
   /// </summary>
    private void DetectMode()
    {
        Debug.Log("detect mode on");
        detectTime -= Time.deltaTime;
        if (whichDirection < 0)
        {
            transform.position -= new Vector3(0.05f, 0);
        }
        else
        {
            transform.position += new Vector3(0.05f, 0);
        }
        
        if (detectTime < 0)
        {
            Debug.Log("Detect Mode done");
            target = null;
            detectMode = false;
        }

       
    }

    /// <summary>
    /// Patrol the area by moving to each waypoint
    /// </summary>
    private void Walk()
    {
        // Check which waypoint the current one is and move to the other one
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

    void OnBecameVisible()
    {
        Debug.Log("I am visible");
        seeEnemyEvent.Invoke("Get past enemy");
    }

   public void addEnemylistener(UnityAction<string> method)
    {
        seeEnemyEvent.AddListener(method);
    }
}
