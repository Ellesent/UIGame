using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class LockedTypes
{
    public enum LockedStatus { Locked, NotLocked}
}

public class Items
{
    public enum ItemStatus { NotPickedUp, PickedUp}
}


public static class ItemManager
{
   public static Dictionary<string, LockedTypes.LockedStatus> lockedItems = new Dictionary<string, LockedTypes.LockedStatus>();

   public static Dictionary<string, Items.ItemStatus> pickupItems = new Dictionary<string, Items.ItemStatus>();
}
