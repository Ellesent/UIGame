using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class DoorScript : MonoBehaviour {

    public string sceneName;
    //public bool isLocked;
    public LockedTypes.LockedStatus locked;
    Inventory inventory;
	// Use this for initialization
	void Start () {

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
        if (collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Space))
        {
            if (ItemManager.lockedItems[name] == LockedTypes.LockedStatus.NotLocked)
            {
                SceneData.previousScene = SceneManager.GetActiveScene().name;
                SceneData.currentScene = sceneName;
                SceneManager.LoadScene(sceneName);
            }
            else
            {

                if (inventory.IsItemInInventory("tempkey"))
                {
                    inventory.RemoveItem("tempkey");
                    ItemManager.lockedItems[name] = LockedTypes.LockedStatus.NotLocked;
                }
            }
        }
    }

}
