using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CraftManager
{

    public Transform spawnPoint;
    [HideInInspector] public int playerNumber;
    [HideInInspector] public GameObject m_Instance;
    [HideInInspector] public int m_Wins;

    public bool isJoined { get; set; }



    private CraftMovement movement;
    private Shoot shooting;
    private GameObject m_CanvasGameObject;


    public void Setup()
    {
        movement = m_Instance.GetComponent<CraftMovement>();
        shooting = m_Instance.GetComponent<Shoot>();

        movement.playerNumber = playerNumber;
        shooting.playerNumber = playerNumber;

    }


    public void DisableControl()
    {
        movement.enabled = false;
        shooting.enabled = false;

    }


    public void EnableControl()
    {
        movement.enabled = true;
        shooting.enabled = true;

    }


    public void Reset()
    {
        m_Instance.transform.position = spawnPoint.position;
        m_Instance.transform.rotation = spawnPoint.rotation;

        m_Instance.SetActive(false);
        m_Instance.SetActive(true);
    }

    public CraftManager(int _playerNumber)
    {
        playerNumber = _playerNumber;
    }
}
