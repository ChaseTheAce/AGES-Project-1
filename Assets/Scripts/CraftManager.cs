using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CraftManager
{

    public Color m_PlayerColor;
    public Transform m_SpawnPoint;
    [HideInInspector] public int m_PlayerNumber;
    [HideInInspector] public string m_ColoredPlayerText;
    [HideInInspector] public GameObject m_Instance;
    [HideInInspector] public int m_Wins;


    private CraftMovement movement;
    private Shoot shooting;
    private GameObject m_CanvasGameObject;


    public void Setup()
    {
        movement = m_Instance.GetComponent<CraftMovement>();
        shooting = m_Instance.GetComponent<Shoot>();
        //m_CanvasGameObject = m_Instance.GetComponentInChildren<Canvas>().gameObject;

        movement.playerNumber = m_PlayerNumber;
        shooting.playerNumber = m_PlayerNumber;

        //m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColor) + ">PLAYER " + m_PlayerNumber + "</color>";

        //MeshRenderer[] renderers = m_Instance.GetComponentsInChildren<MeshRenderer>();

        //for (int i = 0; i < renderers.Length; i++)
        //{
        //    renderers[i].material.color = m_PlayerColor;
        //}
    }


    public void DisableControl()
    {
        movement.enabled = false;
        shooting.enabled = false;

        //m_CanvasGameObject.SetActive(false);
    }


    public void EnableControl()
    {
        movement.enabled = true;
        shooting.enabled = true;

        //m_CanvasGameObject.SetActive(true);
    }


    public void Reset()
    {
        m_Instance.transform.position = m_SpawnPoint.position;
        m_Instance.transform.rotation = m_SpawnPoint.rotation;

        m_Instance.SetActive(false);
        m_Instance.SetActive(true);
    }
}
