using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheel : MonoBehaviour
{
	[SerializeField] private Transform wheelTransform;
	[SerializeField] private bool isSteer;
	[SerializeField] private bool isInvertSteer;
	[SerializeField] private bool isPower;

	private float steerAngel;
	private float motorTorque;
	private WheelCollider wheelCollider;

	void Start()
	{
		wheelCollider = GetComponent<WheelCollider>();
	}
	void Update()
	{
		wheelCollider.GetWorldPose(out Vector3 pos, out Quaternion rotate);
		wheelTransform.position = pos;
		wheelTransform.rotation = rotate * Quaternion.Euler(0, 0, -90);
	}
	void FixedUpdate()
	{
		if (isSteer)
		{
			wheelCollider.steerAngle = steerAngel * (isInvertSteer ? -1 : 1);
		}

		if (isPower)
		{
			wheelCollider.motorTorque = motorTorque;
		}

	}
	public void ChangeMotorTorque(float value)
	{
		motorTorque = value;
	}

	public void ChangeAngelSteer(float value)
	{
		steerAngel = value;
	}
}