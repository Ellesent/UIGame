using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneTransition : MonoBehaviour {

    public string sceneName;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Transfer to another scene if player collides with object (used for death)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (sceneName == "Death")
            {
                SceneData.numDeaths++;
                Destroy(GameObject.Find("EventSystem"));
            }
            SceneData.previousScene = SceneManager.GetActiveScene().name;
            SceneData.currentScene = sceneName;
            SceneManager.LoadScene(sceneName);
        }
    }

}
