using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour {

    static MusicScript instance;

	// Use this for initialization
	void Awake () {
	    if(instance != null)
        {
            Destroy(gameObject);
        }else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
