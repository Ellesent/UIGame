using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour {

	// Use this for initialization
	void Start () {

        if (!ItemManager.pickupItems.ContainsKey(name))
        {
            ItemManager.pickupItems.Add(name, Items.ItemStatus.NotPickedUp);
        }

        if(ItemManager.pickupItems[name] == Items.ItemStatus.PickedUp)
        {
            Destroy(gameObject);
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Destroy()
    {
        ItemManager.pickupItems[name] = Items.ItemStatus.PickedUp;
        Destroy(gameObject);
    }
}
