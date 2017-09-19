using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour {
    List<string> inventory = new List<string>();

    public GameObject inventoryImage;

    public bool IsItemInInventory(string item)
    {
        Debug.Log("Inventory: " + inventory);
        return inventory.Contains(item);
    }
    // Use this for initialization
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Adds the item based on the name of the game object
    public void AddItem(string item)
    {
        inventory.Add(item);
        AddImagetoInventory(item);
    }

    // Removes the item based on the name of the game object
    public void RemoveItem(string item)
    {
        inventory.Remove(item);
    }

    // Adds image to inventory based on the name of the sprite in the Resources folder
    void AddImagetoInventory(string sprite)
    {
        Debug.Log("Sprites/" + sprite);
        Sprite x = Resources.Load<Sprite>("Sprites/" + sprite);
        
        if (x == null)
        {
           
        }
        for (int i = 0; i < inventoryImage.transform.childCount; i++)
        {
            if (inventoryImage.transform.GetChild(i).GetComponent<Image>()!= null && inventoryImage.transform.GetChild(i).GetComponent<Image>().sprite == null)
            {
                Debug.Log("Got in");
                inventoryImage.transform.GetChild(i).GetComponent<Image>().sprite = x;
                break;
            }
        }
    }
}
