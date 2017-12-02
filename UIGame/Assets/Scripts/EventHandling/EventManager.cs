using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager {

    // Variables for Enter House event
    static Player thisEnterHouseInvoker;
    static UnityAction<string, string> thisEnterHouseListener;

    // Variables for Pickup Key event
    static Player keyInvoker;
    static UnityAction<string> keyListener;

    // Variables for see enemy event
    static Enemy enemyInvoker;
    static UnityAction<string> enemyListener;

    // Variables for using key event
    static Player doorInvoker;
    static UnityAction<string, string> doorListener;


    /// <summary>
    /// Enter House Invoker 
    /// </summary>
    /// <param name="invoker"></param>
    public static void EnterHouseInvoker(Player invoker)
    {
        thisEnterHouseInvoker = invoker;

        if (thisEnterHouseListener != null)
        {
            invoker.AddEnterHouseListener(thisEnterHouseListener);
        }
    }

    /// <summary>
    /// Enter House Listener - Mark Quest As Complete
    /// </summary>
    /// <param name="method"></param>
    public static void EnterHouseListener(UnityAction<string, string> method)
    {
        thisEnterHouseListener = method;

        if (thisEnterHouseInvoker != null)
        {
            thisEnterHouseInvoker.AddEnterHouseListener(method);
        }
    }

    public static void PickupKeyInvoker(Player invoker)
    {
        keyInvoker = invoker;
        if (keyListener != null)
        {
            invoker.AddKeyListener(keyListener);
        }
    }

    public static void PickupKeyListener(UnityAction<string> method)
    {
        keyListener = method;
        if (keyInvoker != null)
        {
            keyInvoker.AddKeyListener(method);
        }
    }

    public static void EnemyInvoker(Enemy invoker)
    {
        enemyInvoker = invoker;
        if (enemyListener != null)
        {
            invoker.addEnemylistener(keyListener);
        }
    }

    public static void EnemyListener(UnityAction<string> method)
    {
        enemyListener = method;
        if (enemyInvoker != null)
        {
            enemyInvoker.addEnemylistener(method);
        }
    }

    public static void DoorInvoker(Player invoker)
    {
        doorInvoker = invoker;
        if (doorListener != null)
        {
            invoker.AddDoorListener(doorListener);
        }
    }

    public static void DoorListener(UnityAction<string, string> method)
    {
        doorListener = method;
        if (doorInvoker != null)
        {
            doorInvoker.AddDoorListener(method);
        }
    }



} 

