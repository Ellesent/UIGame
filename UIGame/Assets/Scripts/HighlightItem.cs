using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class HighlightItem : MonoBehaviour, ISelectHandler, IDeselectHandler
{

    bool select;

    public Image image;
    Color origColor;

    // Use this for initialization
    void Start()
    {
        select = false;
        origColor = image.GetComponent<Image>().color;
        
    }

    // Update is called once per frame
    void Update() {

        if (select)
        {
            // Highlight child image if there is an object in this inventory space
            if (GetComponent<Image>().sprite != null)
            {
                image.color = Color.red;
            }
        }
    }


    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("I am being highlighted");
        select = true;

        

    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log("I am not selected");
        image.color = origColor;
        select = false;
    }

}

