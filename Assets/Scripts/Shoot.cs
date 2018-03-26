using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour {

    public int playerNumber = 1;
    public Rigidbody shot;
    public Transform fireTransform;
    //public Slider aimSlider;
    //public AudioSource shootingAudio;
    //public AudioClip chargingClip;
    //public AudioClip fireClip;
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
        //aimSlider.value = minLaunchForce;
    }


    private void Start()
    {
        m_FireButton = "Fire" + playerNumber;

        m_ChargeSpeed = (maxLaunchForce - minLaunchForce) / maxChargeTime;
    }


    private void Update()
    {
        // Track the current state of the fire button and make decisions based on the current launch force.
        //aimSlider.value = minLaunchForce;

        if (m_CurrentLaunchForce >= maxLaunchForce && !m_Fired)
        {
            m_CurrentLaunchForce = maxLaunchForce;
            Fire();
        }
        else if (Input.GetButtonDown(m_FireButton))
        {
            m_Fired = false;
            m_CurrentLaunchForce = minLaunchForce;

            //shootingAudio.clip = chargingClip;
            //shootingAudio.Play();
        }
        else if (Input.GetButton(m_FireButton) && !m_Fired)
        {
            m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;

            //aimSlider.value = m_CurrentLaunchForce;
        }
        else if (Input.GetButtonUp(m_FireButton) && !m_Fired)
        {
            Fire();
        }
    }


    private void Fire()
    {
        // Instantiate and launch the shell.
        m_Fired = true;

        Rigidbody shellInstance = Instantiate(shot, fireTransform.position, fireTransform.rotation) as Rigidbody;

        shellInstance.velocity = m_CurrentLaunchForce * fireTransform.forward;

        //shootingAudio.clip = fireClip;
        //shootingAudio.Play();

        m_CurrentLaunchForce = minLaunchForce;

    }
}
