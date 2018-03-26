using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour {

    public int playerNumber = 1;
    public Rigidbody shot;
    public Transform fireTransform;
    public float minLaunchForce = 15f;
    public float maxLaunchForce = 30f;
    public float maxChargeTime = 0.75f;


    private string m_FireButton;
    private float m_CurrentLaunchForce;
    private float m_ChargeSpeed;
    private bool m_Fired;


    private void OnEnable()
    {
        m_CurrentLaunchForce = minLaunchForce;
    }


    private void Start()
    {
        m_FireButton = "Fire" + playerNumber;

        m_ChargeSpeed = (maxLaunchForce - minLaunchForce) / maxChargeTime;
    }


    private void Update()
    {

        if (m_CurrentLaunchForce >= maxLaunchForce && !m_Fired)
        {
            m_CurrentLaunchForce = maxLaunchForce;
            Fire();
        }
        else if (Input.GetButtonDown(m_FireButton))
        {
            m_Fired = false;
            m_CurrentLaunchForce = minLaunchForce;
;
        }
        else if (Input.GetButton(m_FireButton) && !m_Fired)
        {
            m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;

        }
        else if (Input.GetButtonUp(m_FireButton) && !m_Fired)
        {
            Fire();
        }
    }


    private void Fire()
    {
        m_Fired = true;

        Rigidbody shellInstance = Instantiate(shot, fireTransform.position, fireTransform.rotation) as Rigidbody;

        shellInstance.velocity = m_CurrentLaunchForce * fireTransform.forward;

        m_CurrentLaunchForce = minLaunchForce;

    }
}
