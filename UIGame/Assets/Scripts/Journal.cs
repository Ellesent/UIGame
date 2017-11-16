using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Journal : MonoBehaviour {

    bool isClosed;
    float timeStartedLerping;
    RectTransform transformRect;
    Vector2 openPosition;
    Vector2 closedPosition;
	// Use this for initialization
	void Start () {

        isClosed = false;
        transformRect = GetComponent<RectTransform>();
        openPosition = transformRect.anchoredPosition;
        closedPosition = new Vector2(openPosition.x - 200, openPosition.y);
		
	}
	
	// Update is called once per frame
	void Update () {
        ToggleJournal();
		
	}

    void ToggleJournal()
    {
        if (Input.GetKeyDown(InputFields.seeJournal))
        {
            isClosed = !isClosed;
            timeStartedLerping = Time.time;
            
        }

        animateJournal(isClosed);
    }

    void animateJournal(bool closing)
    {
       
        float timeSinceStarted = Time.time - timeStartedLerping;
        float timeTakenDuringLerp = 1f;
        float percentageComplete = timeSinceStarted / timeTakenDuringLerp;
        if (closing)
        {
            transformRect.anchoredPosition = Vector2.Lerp(openPosition, closedPosition, percentageComplete);
        }
        else
        {
            transformRect.anchoredPosition = Vector2.Lerp(closedPosition, openPosition, percentageComplete);
        }
    }
}
