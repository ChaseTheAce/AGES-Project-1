using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{

    public float speed = 5;


    #region Fields
    private float xInput;
    private float yInput;
    private Vector3 moveDirection;

    public int playerNumber;

    Rigidbody rigidbody;
    #endregion

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    #region Monobehaviour functions
    private void Update()
    {
        GetInput();
    }

    private void OnEnable()
    {
        rigidbody.isKinematic = false;
        moveDirection = new Vector3(0, 0, 0);
    }

    private void OnDisable()
    {
        rigidbody.isKinematic = true;
    }

    private void FixedUpdate()
    {
        UpdateRotation();
        UpdateMovement();
        
    }
    #endregion

    private void GetInput()
    {
        xInput = Input.GetAxis("Horizontal" + playerNumber);
        yInput = Input.GetAxis("Vertical" + playerNumber);

        moveDirection = new Vector3(xInput, 0, yInput);
    }

    private void ConvertInputToCameraRelative()
    {
        moveDirection = Camera.main.transform.InverseTransformDirection(moveDirection);
    }

    private void UpdateRotation()
    {
        float turnThreshold = 0.1f;
        if (moveDirection.magnitude > turnThreshold && moveDirection != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(moveDirection);

            rigidbody.transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * speed);
        }
    }

    private void UpdateMovement()
    {
        rigidbody.MovePosition(rigidbody.position + (moveDirection * speed * Time.deltaTime));
    }

}
