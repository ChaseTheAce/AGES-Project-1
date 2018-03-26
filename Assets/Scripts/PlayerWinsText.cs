using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWinsText : MonoBehaviour {

    Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    // Use this for initialization
    void Start ()
    {
        text.text = "Player " + GameManager.winner.playerNumber + " Won!";
	}
	
}
