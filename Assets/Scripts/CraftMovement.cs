﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftMovement : MonoBehaviour 
{
    public int playerNumber = 1;
    public float speed = 12f;
    public float turnSpeed = 180f;

    private string movementAxisName;
    private string turnAxisName;
    private Rigidbody myRigidbody;
    private float movementInputValue;
    private float turnInputValue;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        myRigidbody.isKinematic = false;
        movementInputValue = 0f;
        turnInputValue = 0f;
    }

    private void OnDisable()
    {
        myRigidbody.isKinematic = true;
    }

    // Use this for initialization
    void Start () 
	{
        movementAxisName = "Vertical" + playerNumber;
        turnAxisName = "Horizontal" + playerNumber;
    }
	
	// Update is called once per frame
	void Update () 
	{
        movementInputValue = Input.GetAxis(movementAxisName);
        turnInputValue = Input.GetAxis(turnAxisName);
	}

    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        Vector3 movement = transform.forward * movementInputValue * speed * Time.deltaTime;

        myRigidbody.MovePosition(myRigidbody.position + movement);
    }

    private void Turn()
    {
        float turn = turnInputValue * turnSpeed * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        myRigidbody.MoveRotation(myRigidbody.rotation * turnRotation);
    }
}
