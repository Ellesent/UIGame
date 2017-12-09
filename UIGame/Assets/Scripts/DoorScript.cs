using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Events;

public class DoorScript : MonoBehaviour {

    public string sceneName;
    //public bool isLocked;
    public LockedTypes.LockedStatus locked;
    Inventory inventory;

    TutorialBehavior tutorialText;

    // Event Handling
  
	// Use this for initialization
	void Start () {

        try
        {
            tutorialText = GameObject.Find("TutorialText").GetComponent<TutorialBehavior>();
        }
        catch (NullReferenceException)
        {
            {
                Debug.Log("Can't find tutorial text, moving on");
            }

        }



        if (!ItemManager.lockedItems.ContainsKey(name))
        {
            ItemManager.lockedItems.Add(name, locked);
        }
        //ItemManager.lockedItems.Add(name, isLocked);
        if (inventory == null)
        {
            try
            {
                inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
            }
            catch (NullReferenceException)
            {
                Debug.Log("Can't find inventory, moving on");
            }

        }

    }
	
	// Update is called once per frame
	void Update () {

		
	}

    // TODO: Change OnTriggerStay to not check for input
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetKeyDown(InputFields.interact))
        {
            if (ItemManager.lockedItems[name] == LockedTypes.LockedStatus.NotLocked)
            {
                SceneData.previousScene = SceneManager.GetActiveScene().name;
                SceneData.currentScene = sceneName;
                SceneManager.LoadScene(sceneName);
            }
            else
            {
              
                if (inventory.GetEquippedItem() == "tempkey")
                {
                    inventory.RemoveItem("tempkey");
                   
                    ItemManager.lockedItems[name] = LockedTypes.LockedStatus.NotLocked;
                }
                else
                {
                    SceneData.numTriesOpeningDoor++;
                    GetComponent<AudioSource>().Play();

                    if (SceneData.numTriesOpeningDoor >= 3)
                    {
                        tutorialText.SetText("HINT: If a door is locked, you will have to use a key. Make sure to equip one from the inventory before trying to open a locked door.");
                    }
                }
            }
        }
    }

    

}
