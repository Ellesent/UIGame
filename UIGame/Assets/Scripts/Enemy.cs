using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameObject player;

    Transform target;

    float minRange = 5;
    float move = 4;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (target  == null)
        {
            //TODO walk on path
        }

        else
        {
            //transform.LookAt(target);
            float distance = Vector2.Distance(transform.position, target.position);
            float movementDistance = move * Time.deltaTime;
            transform.position =  Vector2.MoveTowards(transform.position, target.position, movementDistance);
        }
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.gameObject.tag == "Player")
        {
            target = player.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            target = null;
        }
    }

    //TODO will be used for when enemy loses player, but it still searching for player
    private void DetectMode()
    {

    }
}
