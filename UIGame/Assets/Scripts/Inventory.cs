using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour {
    List<string> inventory = new List<string>();
    string equippedItem = "";

    int numEdibles = 0;

    public GameObject inventoryImage;
    public GameObject equippedImage;

    public GameObject edibleText;

    #region Properties
    public string GetEquippedItem()
    {
        return equippedItem;
    }
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

    #endregion

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

        edibleText.GetComponent<Text>().text = numEdibles.ToString();
		
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
        ChangeImageInInventory(oldItem, newItem);
    }

    // Adds image to inventory based on the name of the sprite in the Resources folder
    void AddImagetoInventory(string sprite)
    {
  
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

    public void AddImageToEquipped(Image image)
    {
        if (image.GetComponent<Image>().sprite != null)
        {

            string spriteName = image.GetComponent<Image>().sprite.name;
            Debug.Log("Sprites/" + spriteName);
            equippedImage.GetComponent<Image>().sprite = image.sprite;
            equippedItem = spriteName;
        }
    }

    // Changes an item's inventory image. This is used for combining
    void ChangeImageInInventory(string oldImage, string newImage)
    {
        Sprite newSprite = Resources.Load<Sprite>("Sprites/" + newImage);

        Debug.Log("New sprite is " + newSprite);
        for (int i = 0; i < inventoryImage.transform.childCount; i++)
        {
            if (inventoryImage.transform.GetChild(i).GetComponent<Image>() != null && inventoryImage.transform.GetChild(i).GetComponent<Image>().sprite != null && inventoryImage.transform.GetChild(i).GetComponent<Image>().sprite.name == oldImage)
            {
                Debug.Log("checking child named " + inventoryImage.transform.GetChild(i).ToString());
                Debug.Log("Changing image");
                inventoryImage.transform.GetChild(i).GetComponent<Image>().sprite = newSprite;
                break;
            }
        }
    }
}
