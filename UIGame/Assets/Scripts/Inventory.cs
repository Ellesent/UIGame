using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour {
    List<string> inventory = new List<string>();

    int numEdibles = 0;

    public GameObject inventoryImage;

    public int GetNumEdibles()
    {
        return numEdibles;
    }

    public void AddNumEdibles()
    {
        numEdibles++;
    }

    public void RemovedNumEdibles()
    {
        numEdibles--;
    }
    

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

    // Used to change the name of an inventory item - this is used for combining
    public void ChangeItemName(string oldItem, string newItem)
    {
        inventory.Remove(oldItem);
        inventory.Add(newItem);
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

    // Changes an item's inventory image. This is used for combining
    void ChangeImageInInventory(string oldImage, string newImage)
    {
        Sprite newSprite = Resources.Load<Sprite>("Sprites/" + newImage);
        for (int i = 0; i < inventoryImage.transform.childCount; i++)
        {
            if (inventoryImage.transform.GetChild(i).GetComponent<Image>() != null && inventoryImage.transform.GetChild(i).GetComponent<Image>().sprite.name == oldImage)
            {
                Debug.Log("Got in");
                inventoryImage.transform.GetChild(i).GetComponent<Image>().sprite = newSprite;
                break;
            }
        }
    }
}
