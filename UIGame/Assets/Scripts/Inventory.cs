using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour {
    List<string> inventory = new List<string>();

    public GameObject inventoryImage;
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

    public void AddItem(string item)
    {
        inventory.Add(item);
        AddImagetoInventory(item);
    }

    public void RemoveItem(string item)
    {
        inventory.Remove(item);
    }

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
