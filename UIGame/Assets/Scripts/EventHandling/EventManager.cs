using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager {

    static Player thisEnterHouseInvoker;
    static UnityAction<string, string> thisEnterHouseListener;
    static UnityAction<string> thisEnterHouseListenerSettingNextQuest;

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


    /// <summary>
    /// Enter House Listener - set quest
    /// </summary>
    /// <param name="method"></param>
    //public static void EnterHouseListenerNextQuest(UnityAction<string> method)
    //{
    //    thisEnterHouseListenerSettingNextQuest = method;

    //    if (thisEnterHouseInvoker != null)
    //    {
    //        thisEnterHouseInvoker.AddEnterHouseListener(method);
    //    }
    //}

} 

