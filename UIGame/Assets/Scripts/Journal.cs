using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Journal : MonoBehaviour {

    bool isClosed;
    bool setNextQuest = false;
    
    // Stuff for Lerping journal open and closed
    float timeStartedLerping;
    RectTransform transformRect;
    Vector2 openPosition;
    Vector2 closedPosition;
    EnterHouseSetNextQuestEvent setExploreHouseEvent;

	// Use this for initialization
	void Start () {

        // Start out open, and set open and closed positions
        isClosed = false;
        transformRect = GetComponent<RectTransform>();
        openPosition = transformRect.anchoredPosition;
        closedPosition = new Vector2(openPosition.x - 200, openPosition.y);

        CreateQuestList();
        DrawQuest(QuestManager.currentQuest);

        setExploreHouseEvent = new EnterHouseSetNextQuestEvent();
        setExploreHouseEvent.AddListener(SetQuest);
        EventManager.EnterHouseListener(MarkQuestAsComplete);
        //EventManager.EnterHouseListenerNextQuest(SetQuest);
		
	}
	
	// Update is called once per frame
	void Update () {
        ToggleJournal();

        if (setNextQuest)
        {
            Debug.Log(transform.GetChild(2).GetComponent<CanvasRenderer>().GetAlpha());
            if (transform.GetChild(2).GetComponent<CanvasRenderer>().GetAlpha() == 0)
            {
                Debug.Log("AM I IN");
                setExploreHouseEvent.Invoke("Explore the house");
                setNextQuest = false;
            }
        }
		
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

    void OpenJournalForQuestCompletion()
    {

        isClosed = false;
        timeStartedLerping = Time.time;
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

        // Enter Quests House
        QuestManager.quests.Add("Enter the house", new Dictionary<string, bool>());
        QuestManager.quests["Enter the house"].Add("You have just arrived at the address, and there is only a house. Enter through the front door.", false);

        // Explore the house quest
        QuestManager.quests.Add("Explore the house", new Dictionary<string, bool>());
        QuestManager.quests["Explore the house"].Add("You've stepped inside the house. Something doesn't feel right...", false);

        // Pickup Key 
        QuestManager.quests.Add("Find out where the key goes", new Dictionary<string, bool>());
        QuestManager.quests["Find out where the key goes"].Add("You have found a key. This probably opens a door somewhere", false);

        // Get past Enemy
        QuestManager.quests.Add("Get past enemy", new Dictionary<string, bool>());
        QuestManager.quests["Get past enemy"].Add("That thing doesn't look friendly, I better stay out of its sight", false);

        // Grow plant 
        QuestManager.quests.Add("Grow the plant", new Dictionary<string, bool>());
        QuestManager.quests["Grow the plant"].Add("That plant looks weird. Maybe if I put some water on it, it will grow", false);
        QuestManager.quests["Grow the plant"].Add("Find Water Bottle", false);
        QuestManager.quests["Grow the plant"].Add("Fill Water Bottle", false);
        QuestManager.quests["Grow the plant"].Add("Spray Plant", false);



    }

   /// <summary>
   /// Mark a certain quest as complete
   /// </summary>
   /// <param name="topQuestName">The first key in the dictionary's name</param>
   /// <param name="subQuestName">The subquest in nested dictionary</param>
    void MarkQuestAsComplete(string topQuestName, string subQuestName)
    {
        Debug.Log("quest is complete");
        //Mark quest as complete (change bool)

        QuestManager.quests[topQuestName][subQuestName] = true;

        //Check if all subquests under quest are complete. If so...

        int items = QuestManager.quests[topQuestName].Count;

        int itemsCompleted = 0;

        foreach (KeyValuePair<string, bool> quest in QuestManager.quests[topQuestName])
        {
            if (quest.Value == true)
            {
                itemsCompleted++;
            }
        }

        if (itemsCompleted == items)
        {
            //Show Text
            DrawQuest(topQuestName, false);
            // Play sound
            GetComponent<AudioSource>().Play();

            // open Journal
            OpenJournalForQuestCompletion();
            AnimateJournal(false);

            //Click checkmark
            GetComponentInChildren<Toggle>().isOn = true;



            // Fade out text
            Text[] text = GetComponentsInChildren<Text>();

            foreach(Text questText in text)
            {
                questText.CrossFadeAlpha(0.0f, 5.0f, false);
            }

            if (topQuestName == "Enter the house")
            {
                setNextQuest = true;
            }
        }
    }

    // Set the next quest
    void SetQuest(string questName)
    {
        GetComponentInChildren<Toggle>().isOn = false;
        QuestManager.currentQuest = questName;
        DrawQuest(QuestManager.currentQuest);
    }

    /// <summary>
    /// Draw Quest on Journal from list of quests
    /// </summary>
    /// <param name="QuestName">The first key quest name in dictionary</param>
    void DrawQuest(string QuestName, bool shouldFadeIn=true)
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

            // If the text should fade in, do so
            if (shouldFadeIn)
            {
                text.gameObject.GetComponent<CanvasRenderer>().SetAlpha(0);
                text.CrossFadeAlpha(1.0f, 2.0f, false);
            }
        }
    }
}
