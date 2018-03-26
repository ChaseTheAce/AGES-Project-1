using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour {

    public int numRoundsToWin = 5;
    public float startDelay = 3f;
    public float endDelay = 3f;
    public float timeBetweenPowerups = 10f;
    public CameraController cameraControl;
    public Text messageText;
    public GameObject playerPrefab;
    public CraftManager[] crafts;


    public Powerup pwrUp1;
    public Powerup pwrUp2;
    public Powerup pwrUp3;


    private int roundNumber;
    private WaitForSeconds startWait;
    private WaitForSeconds endWait;
    private CraftManager roundWinner;
    private CraftManager gameWinner;


    private static List<Player> allPlayers;

    public static int NumberOfJoinedPlayers
    {
        get
        {
            return allPlayers.Where(player => player.IsJoined).Count();
        }
    }


    private void Start()
    {
        startWait = new WaitForSeconds(startDelay);
        endWait = new WaitForSeconds(endDelay);

        SpawnAllCraft();
        SetCameraTargets();

        StartCoroutine(GameLoop());
    }


    private void SpawnAllCraft()
    {
        for (int i = 0; i < crafts.Length; i++)
        {
            crafts[i].m_Instance =
                Instantiate(playerPrefab, crafts[i].spawnPoint.position, crafts[i].spawnPoint.rotation) as GameObject;
            crafts[i].playerNumber = i + 1;
            crafts[i].Setup();
        }
    }


    private void SetCameraTargets()
    {
        Transform[] targets = new Transform[crafts.Length];

        for (int i = 0; i < targets.Length; i++)
        {
            targets[i] = crafts[i].m_Instance.transform;
        }

        cameraControl.targets = targets;
    }


    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        if (gameWinner != null)
        {
            SceneManager.LoadScene(3);
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }


    private IEnumerator RoundStarting()
    {
        ResetAllCraft();
        DisableCraftControl();
        pwrUp1.Reset();
        pwrUp2.Reset();
        pwrUp3.Reset();

        cameraControl.SetStartPositionAndSize();

        roundNumber++;
        messageText.text = "Round " + roundNumber;

        yield return startWait;
    }


    private IEnumerator RoundPlaying()
    {
        EnableCraftControl();

        messageText.text = string.Empty;

        while (!OneCraftLeft())
        {
            yield return null;
        }


    }


    private IEnumerator RoundEnding()
    {
        DisableCraftControl();

        roundWinner = null;

        roundWinner = GetRoundWinner();

        if (roundWinner != null)
        {
            roundWinner.m_Wins++;
        }

        gameWinner = GetGameWinner();

        string message = EndMessage();
        messageText.text = message;


        yield return endWait;
    }


    private bool OneCraftLeft()
    {
        int numTanksLeft = 0;

        for (int i = 0; i < crafts.Length; i++)
        {
            if (crafts[i].m_Instance.activeSelf)
                numTanksLeft++;
        }

        return numTanksLeft <= 1;
    }


    private CraftManager GetRoundWinner()
    {
        for (int i = 0; i < crafts.Length; i++)
        {
            if (crafts[i].m_Instance.activeSelf)
                return crafts[i];
        }

        return null;
    }


    private CraftManager GetGameWinner()
    {
        for (int i = 0; i < crafts.Length; i++)
        {
            if (crafts[i].m_Wins == numRoundsToWin)
                return crafts[i];
        }

        return null;
    }


    private string EndMessage()
    {
        string message = "DRAW!";

        if (roundWinner != null)
            message = "Player: " + roundWinner.playerNumber + " WINS THE ROUND!";

        message += "\n\n\n\n";

        for (int i = 0; i < crafts.Length; i++)
        {
            message += crafts[i].playerNumber + ": " + crafts[i].m_Wins + " WINS\n";
        }

        if (gameWinner != null)
            message = "Player: " + gameWinner.playerNumber + " WINS THE GAME!";

        return message;
    }


    private void ResetAllCraft()
    {
        for (int i = 0; i < crafts.Length; i++)
        {
            crafts[i].Reset();
        }
    }


    private void EnableCraftControl()
    {
        for (int i = 0; i < crafts.Length; i++)
        {
            crafts[i].EnableControl();
        }
    }


    private void DisableCraftControl()
    {
        for (int i = 0; i < crafts.Length; i++)
        {
            crafts[i].DisableControl();
        }
    }

}

