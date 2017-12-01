using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QuestManager{

   public static Dictionary<string, Dictionary<string, bool>> quests = new Dictionary<string, Dictionary<string, bool>>();

    public static string currentQuest = "Enter the house";
}
