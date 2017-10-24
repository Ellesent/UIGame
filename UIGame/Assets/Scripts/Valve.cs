using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valve : MonoBehaviour {

    public GameObject turnOn;

	// Use this for initialization
	void Start () {

        turnOn.SetActive(false);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetKeyDown(InputFields.interact))
        {
            Debug.Log("yup");
            turnOn.SetActive(true);
        }
    }
}
