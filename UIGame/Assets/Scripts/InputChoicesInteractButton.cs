using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputChoicesInteractButton : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        if (Input.GetJoystickNames().Length > 0 && Input.GetJoystickNames()[0] != "")
        {
            Debug.Log("Joystick connected, length is " + Input.GetJoystickNames().Length);
            Debug.Log("inside names: " + Input.GetJoystickNames()[0]);

            Dropdown movement = GetComponent<Dropdown>();

            movement.options.Clear();
            movement.RefreshShownValue();

            List<string> options = new List<string>();

            options.Add("A Button");
            options.Add("X Button");
            options.Add("B Button");


            movement.AddOptions(options);

            movement.RefreshShownValue();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
