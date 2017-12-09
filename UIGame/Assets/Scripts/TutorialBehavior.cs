using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBehavior : MonoBehaviour {

    Text text;
    float timer;
    bool firstTime = true;
    float secondTime = 2f;

    bool shouldCountDown = true;

    string interactButton;
    public void SetText(string newText)
    {
        if (text == null)
        {
            text = GetComponent<Text>();
         
        }

        text.text = newText;
        timer = 4f;
        shouldCountDown = true;
    }

	// Use this for initialization
	void Start () {
        timer = 4f;
        text = GetComponent<Text>();

        string left = InputFields.left.ToString();
        string right = InputFields.right.ToString();

        if (InputFields.checkJoystick())
        {
            if (InputFields.joystickAxis == "LeftAnalog")
            {
                left = "left analog stick";
               
            }
            else
            {
                left = "right analog stick";
                
            }
            interactButton = "A Button";
            text.text = "Use the " + left + " to move left and right";
        }
        else
        {
            interactButton = InputFields.interact.ToString();
            text.text = "Use " + left + " and " + right + " to move left and right";
        }
		
	}
	
	// Update is called once per frame
	void Update () {

        if (shouldCountDown)
        {
            timer -= Time.deltaTime;
            //Debug.Log(" I am counting down " + timer);
        }

        if (timer <= 0)
        {
            text.text = "";
            shouldCountDown = false;

            if (firstTime == true)
            {
                secondTime -= Time.deltaTime;
                
                if (secondTime <=0)
                {
                    timer = 4f;
                    shouldCountDown = true;
                    text.text = "use " + interactButton + " to pick up and interact with objects";
                    secondTime = 2f;
                    firstTime = false;
                    
                }
            }
        }
	}
}
