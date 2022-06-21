using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float movementSpeed;
	[SerializeField] private Transform orientation;

	private float _horizontalInput;
	private float _verticalInput;

	private Vector3 _moveDirection;

	private Rigidbody _playerRb;

	private void Start()
	{
		_playerRb = GetComponent<Rigidbody>();
		_playerRb.freezeRotation = true;
		_playerRb.drag = 5f;
	}

	private void FixedUpdate()
	{
		MovePlayer();
	}

	private void Update()
	{
		GetInputs();
	}

	private void GetInputs()
	{
		_horizontalInput = Input.GetAxisRaw("Horizontal");
		_verticalInput = Input.GetAxisRaw("Vertical");
	}

	private void MovePlayer()
	{
		_moveDirection = orientation.forward * _verticalInput + orientation.right * _horizontalInput;
		
		_playerRb.AddForce(_moveDirection.normalized * movementSpeed, ForceMode.Force);
	}
}
