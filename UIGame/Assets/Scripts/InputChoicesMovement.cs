using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputChoicesMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {

        if (InputFields.checkJoystick())
        {
            Debug.Log("Joystick connected, length is " + Input.GetJoystickNames().Length);
            Debug.Log("inside names: " + Input.GetJoystickNames()[0]);

            Dropdown movement  = GetComponent<Dropdown>();

            movement.options.Clear();
            movement.RefreshShownValue();

            List<string> options = new List<string>();

            options.Add("left analog");
            options.Add("right analog");

            movement.AddOptions(options);

            movement.RefreshShownValue();

            

        }

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
