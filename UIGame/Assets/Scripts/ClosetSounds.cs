using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetSounds : MonoBehaviour {

    AudioSource source;

    public AudioClip openCloset;
    public AudioClip closeCloset;
	// Use this for initialization
	void Start () {

        source = GetComponent<AudioSource>();
        source.clip = openCloset;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayOpen()
    {
        source.clip = openCloset;
        source.Play();
    }

    public void PlayClosed()
    {
        source.clip = closeCloset;
        source.Play();
    }
}
