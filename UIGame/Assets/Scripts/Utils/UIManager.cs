using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class UIManager : MonoBehaviour {

    public Canvas inventoryCanvas;

	// Use this for initialization
	void Start () {

        if (inventoryCanvas == null)
        {
            try
            {
                inventoryCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            }

            catch (NullReferenceException e)
            {
                Debug.Log("Can't find canvas, moving on");
            }
        }

        if (inventoryCanvas != null)
        {
            inventoryCanvas.enabled = false;
        }
    }
		
	
	
	// Update is called once per frame
	void Update () {

        InventoryGui();
		
	}

   public void ButtonClickScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ButtonClickQuit()
    {
        Application.Quit();
    }

    private void InventoryGui()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryCanvas.enabled = !inventoryCanvas.enabled;
        }
    }
}
