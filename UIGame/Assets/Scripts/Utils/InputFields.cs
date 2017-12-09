using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputFields
{

    public static string joystickAxis = "LeftAnalog";

    public static string inventoryButton = "Tab";

    public static KeyCode left = KeyCode.A;
    public static KeyCode right = KeyCode.D;
    public static KeyCode up = KeyCode.W;
    public static KeyCode down = KeyCode.S;
    public static KeyCode interact = KeyCode.Space;
    public static KeyCode run = KeyCode.LeftShift;
    public static KeyCode openInventory = KeyCode.Tab;
    public static KeyCode openHelp = KeyCode.Escape;
    public static KeyCode openCraft = KeyCode.H;
    public static KeyCode seeJournal = KeyCode.Q; 

    public static bool checkJoystick()
    {
        return (Input.GetJoystickNames().Length > 0 && Input.GetJoystickNames()[0] != "");
    }

    public static void InitialKeys()
    {
        if (checkJoystick())
        {
            if (interact == KeyCode.Space || interact == KeyCode.E || interact == KeyCode.F)
            {
                interact = KeyCode.Joystick1Button0;
            }
            openInventory = KeyCode.Joystick1Button3;
            inventoryButton = "the Y Button";
            openHelp = KeyCode.Joystick1Button6;
            seeJournal = KeyCode.Joystick1Button4;
        }
    }
}
