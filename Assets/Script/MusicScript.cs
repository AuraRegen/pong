using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour {

    static MusicScript instance;

    public AudioSource music;

    private int previousNumber;

    private float musicVolume = 0.5f;

    void Start()
    {
        AudioListener.volume = musicVolume;
    }
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

    void Update()
    {
        AudioListener.volume = musicVolume;
    }
	
    public void SetVolume(float pVolume)
    {
        this.musicVolume = pVolume;
    }


}
