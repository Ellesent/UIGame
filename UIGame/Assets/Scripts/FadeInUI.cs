using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class FadeInUI : MonoBehaviour {

    EventSystem eventSystem;
    bool somethingSelected = false;

	// Use this for initialization
	void Start () {
        
        // set initial alpha to 0 and fade in until full opacity
        GetComponent<CanvasRenderer>().SetAlpha(0.0f);

        if (GetComponent<Image>() != null)
        {
            GetComponent<Image>().CrossFadeAlpha(1.0f, 3.0f, false);
        }
        else
        {
            GetComponent<Text>().CrossFadeAlpha(1.0f, 5.0f, false);
        }

        //Get event system
        eventSystem = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();
    }
		
	
	// Update is called once per frame
	void Update () {

        if(gameObject.name == "Button" && GetComponent<CanvasRenderer>().GetAlpha() == 1 && somethingSelected == false)
        {
            eventSystem.SetSelectedGameObject(gameObject);
            somethingSelected = true;
        }
    }
}
