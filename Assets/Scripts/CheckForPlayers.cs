using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheckForPlayers : MonoBehaviour {

    PlayerMovement Player;

    [SerializeField]
    Text playerOneText;

    [SerializeField]
    Text playerTwoText;

    AudioSource audio;

    bool isEnoughPlayers = false;

    bool isPlayer1Ready = false;

    bool isPlayer2Ready = false;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();

        playerOneText.text = "Player One Press A";
        playerTwoText.text = "Player Two Press A";
    }
	
	// Update is called once per frame
	void Update () {
        PlayersCheck();
    }

    public void AddPlayer(int _playerNumber)
    {
        int playerNumber = _playerNumber;

        if (playerNumber == 1)
        {
            playerOneText.text = "Player One Ready!";
            audio.Play();
            isPlayer1Ready = true;
        }
        if (playerNumber == 2)
        {
            playerTwoText.text = "Player Two Ready!";
            audio.Play();
            isPlayer2Ready = true;
        }

    }

    public void PlayersCheck()
    {
        

            if (Input.GetButtonDown("Fire1"))
            {
                AddPlayer(1);
            }
            if (Input.GetButtonDown("Fire2"))
            {
                AddPlayer(2);
            }

            if (isPlayer1Ready && isPlayer2Ready)
            {
                StartGame();
            }
            
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Scene 1");
    }
}
