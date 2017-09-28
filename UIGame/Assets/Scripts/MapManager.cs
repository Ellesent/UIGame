using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour {

    Color baseColor;
    float time = 0.5f;
    bool blink = false;
    // Use this for initialization
    void Start () {

        
	}
	
	// Update is called once per frame
	void Update () {

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Image>() != null)
            {
                if (transform.GetChild(i).name == SceneData.currentScene)
                {
                    ShowCurrentRoom(transform.GetChild(i).GetComponent<Image>());
                }
                else
                {
                    transform.GetChild(i).GetComponent<Image>().color = Color.white;
                }
            }
           
        }


    }

    public void ShowCurrentRoom(Image room)
    {

        time -= Time.deltaTime;   
        if (blink)
        {
            room.color = Color.red;
        }
        else
        {
            room.color = Color.white;
        }

        if (time < 0)
        {
            blink = !blink;
            time = 0.5f;
        }
        
       
    }
}
