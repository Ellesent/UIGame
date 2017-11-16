using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Journal : MonoBehaviour {

    bool isClosed;
    
    // Stuff for Lerping journal open and closed
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

        CreateQuestList();
        DrawQuest("Enter the house");
		
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

        AnimateJournal(isClosed);
    }

    /// <summary>
    /// Animate the opening and closing of journal through lerping
    /// </summary>
    /// <param name="closing"></param>
    void AnimateJournal(bool closing)
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


    // Create the initial Quest List
    void CreateQuestList()
    {
        // Creating Quests
        QuestManager.quests.Add("Enter the house", new Dictionary<string, bool>());
        QuestManager.quests["Enter the house"].Add("You have just arrived at the address, and there is only a house. Enter through the front door.", false);
    }

    // Mark a quest as complete
    void MarkQuestAsComplete(string QuestName)
    {

    }

    // Draw Quest in Journal
    void DrawQuest(string QuestName)
    {
        Component[] texts = GetComponentsInChildren<Text>();

        foreach (Text text in texts)
        {
            if (text.gameObject.name == "Main Text")
            {
                text.text = QuestName;
            }
            else
            {
                List<string> keyList = new List<string>(QuestManager.quests[QuestName].Keys);
                
                foreach (string quest in keyList)
                {
                    text.text = "";
                    text.text += quest; 
                }
            }
        }
    }
}
