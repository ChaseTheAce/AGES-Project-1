using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    private static MusicManager instance;

    public static MusicManager Instance
    {
        get
        {
            return instance;
        }
 
    }

	// Use this for initialization
	void Start () {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
	}
	
}
