using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
	[SerializeField] private Transform massCenter;
	[SerializeField] private float motorTorque;
	[SerializeField] private float maxSteer;
	private float horizontal;
	private float vertical;
	private Rigidbody rb;
	[SerializeField] wheel[] wheels;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.centerOfMass = massCenter.localPosition;
		wheels = GetComponentsInChildren<wheel>();
	}
	void Update()
	{
		horizontal = Input.GetAxis("Horizontal");
		vertical = Input.GetAxis("Vertical");
		foreach (wheel wh in wheels)
		{
			wh.ChangeAngelSteer(horizontal * maxSteer);
			wh.ChangeMotorTorque(vertical * motorTorque);
		}
	}
}
