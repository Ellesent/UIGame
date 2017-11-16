using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTextOverTime : MonoBehaviour
{

    string text;
    // Use this for initialization

    void Start()
    {
        StartCoroutine(AnimateText("You never were close with your parents, but recently they went missing. The only clues you have are an address and a picture of a man. You decide to set out and find them. You head to the address you were given..."));
    }

    IEnumerator AnimateText(string strComplete)
    {
        int i = 0;
        string str = "";
        while (i < strComplete.Length)
        {
            GetComponent<Text>().text += strComplete[i++];
            yield return new WaitForSeconds(0.08F);
        }
    }
}