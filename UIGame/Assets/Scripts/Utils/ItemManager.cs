using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class LockedTypes
{
     enum LockedStatus { Locked, NotLocked}
}

public class Items
{
    enum ItemStatus { PickedUp, Gone}
}


public static class ItemManager
{
    static Dictionary<string, LockedTypes> lockedItems = new Dictionary<string, LockedTypes>();

    static Dictionary<string, Items> pickupItems = new Dictionary<string, Items>();

}
