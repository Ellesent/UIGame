using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScene : MonoBehaviour {


    GameObject[] objects;

    void Awake()
    {
        objects = GameObject.FindGameObjectsWithTag("DeleteMe");

        foreach (GameObject item in objects)
        {
            Destroy(item);
        }

        QuestManager.quests.Clear();
        QuestManager.currentQuest = "Enter the house";

        ItemManager.lockedItems.Clear();
        ItemManager.pickupItems.Clear();

        SceneData.numTriesOpeningDoor = 0;

    }
	// Use this for initialization
	void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
