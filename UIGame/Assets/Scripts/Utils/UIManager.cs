using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class UIManager : MonoBehaviour
{

    public Canvas inventoryCanvas;

    // Use this for initialization
    void Start()
    {

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
    void Update()
    {

        InventoryGui();

    }

    public void ButtonClickScene(string scene)
    {
        SceneData.previousScene = SceneManager.GetActiveScene().name;
        SceneData.currentScene = scene;
        SceneManager.LoadScene(scene);

    }

    public void ButtonClickQuit()
    {
        Application.Quit();
    }

    private void InventoryGui()
    {
        if (Input.GetKeyDown(InputFields.openInventory))
        {
            inventoryCanvas.enabled = !inventoryCanvas.enabled;
        }

    }

    /// <summary>
    /// Gets Input type for movement
    /// </summary>
    /// <param name="value">a dynamic float taken from the drop down</param>
    public void TakeInputMovement(int value)
    {
        if (Input.GetJoystickNames().Length > 0 && Input.GetJoystickNames()[0] != "")
        {
            switch(value)
            {
                case 0:
                    InputFields.joystickAxis = "LeftAnalog";
                    break;
                case 1:
                    InputFields.joystickAxis = "RightAnalog";
                    break;
            }
        }
        else
        {
            switch (value)
            {
                case 0:
                    Debug.Log("WASD");
                    InputFields.left = KeyCode.A;
                    InputFields.right = KeyCode.D;
                    InputFields.up = KeyCode.W;
                    InputFields.down = KeyCode.S;
                    break;
                case 1:
                    Debug.Log("Arrow Keys");
                    InputFields.left = KeyCode.LeftArrow;
                    InputFields.right = KeyCode.RightArrow;
                    InputFields.up = KeyCode.UpArrow;
                    InputFields.down = KeyCode.DownArrow;
                    break;
                case 2:
                    Debug.Log("IJKL");
                    InputFields.left = KeyCode.J;
                    InputFields.right = KeyCode.L;
                    InputFields.up = KeyCode.I;
                    InputFields.down = KeyCode.K;
                    break;
            }
        }
    }

    public void TakeInputSelect(int value)
    {

        if (InputFields.checkJoystick())
        {
            Debug.Log("what");
            switch(value)
            {
                case 0:
                    InputFields.interact = KeyCode.Joystick1Button0;
                    break;
                case 1:
                    InputFields.interact = KeyCode.Joystick1Button2;
                    break;
                case 2:
                    InputFields.interact = KeyCode.Joystick1Button1;
                    break;
            }
        }
        else
        {
            switch (value)
            {
                case 0:
                    Debug.Log("Space");
                    InputFields.interact = KeyCode.Space;
                    break;
                case 1:
                    Debug.Log("E");
                    InputFields.interact = KeyCode.E;
                    break;
                case 2:
                    Debug.Log("F");
                    InputFields.interact = KeyCode.F;
                    break;
            }
        }
    }
}
