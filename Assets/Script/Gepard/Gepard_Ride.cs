using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gepard_Ride : MonoBehaviour
{

	private float horizontal;
	private float vertical;
	[SerializeField] private float speed = 6f;
	[SerializeField] private float speedRotation = 1.5f;
	[SerializeField] private Rigidbody rb;
	public bool canRide = true;
	[SerializeField] private GameObject plane;
	private Vector3 directionForward = new Vector3(-1, 0, 0);
	private Vector3 directionBack = Vector3.forward;
	[SerializeField] private ParticleSystem explosion;
    [SerializeField] private ParticleSystem[] fire;
	[SerializeField] private float sciFiCarHP;
	[SerializeField] private AudioSource engine1;
    [SerializeField] private AudioSource expl;
    [SerializeField] private AudioSource fire_sound;


    void Start()
	{
        engine1.Play();
        canRide = true;
		rb = GetComponent<Rigidbody>();
		plane.SetActive(false);
	}

	private void OnCollisionEnter(Collision collision)
	{
	


        if (collision.transform.CompareTag("Drone") || collision.transform.CompareTag("Bullet"))
		{
			if(canRide)
			{
				engine1.Stop();
				expl.Play();
				explosion.Play();
                fire_sound.Play();
                foreach (var f in fire)
				{
					f.Play();
				}
				Debug.Log("Boom");
				canRide = false;
			}
		}
	}


	void Update()
	{
		if (canRide)
		{
			float forw = Input.GetAxis("Vertical");
			transform.Translate(directionForward * Time.deltaTime * speed * forw);
			float h = Input.GetAxis("Horizontal") * speedRotation;
			Quaternion rotate = transform.rotation * Quaternion.Euler(0, h * speedRotation, 0);
			transform.rotation = Quaternion.Lerp(transform.rotation, rotate, 15 * Time.deltaTime);
		}
		else
		{
			plane.SetActive(true);
		}
	}
}
