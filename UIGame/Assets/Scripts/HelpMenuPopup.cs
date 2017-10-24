using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpMenuPopup : MonoBehaviour {


    Canvas canvas;

	// Use this for initialization
	void Start () {

        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
	}

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(InputFields.openHelp))
        {
            canvas.enabled = !canvas.enabled;
        }
    }
}
