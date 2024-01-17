using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gepard_Ride : MonoBehaviourPunCallbacks
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

	[SerializeField] private PhotonView photonView1;

	[SerializeField] private GameObject PauseMeny;



    void Start()
	{
        if (!photonView1.IsMine)
        {
            Destroy(plane);
        }
        engine1.Play();
        canRide = true;
		rb = GetComponent<Rigidbody>();
		plane.SetActive(false);
		Debug.Log(photonView1.IsMine);

		
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

			

               
                    photonView1.RPC(nameof(NotifyCollision), RpcTarget.All);

					Debug.Log("RPC_AA");

             


                canRide = false;
            }
		}
	}


	


    [Photon.Pun.PunRPC]
    private void NotifyCollision()
    {

        if (canRide)
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

    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Lobbi");

        Cursor.lockState = CursorLockMode.None;
    }

    public void OpenM()
    {
		PauseMeny.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
		

    }

    public void CloseM()
    {
        PauseMeny.SetActive(false);
       
		Cursor.lockState = CursorLockMode.Locked;
        

    }



    void FixedUpdate()
	{

        if (!photonView1.IsMine || PauseMeny.activeSelf)
        {
			return;
        }


        if (Input.GetKey(KeyCode.Escape))
		{
			OpenM();
		}
    
		if(PhotonNetwork.CurrentRoom.PlayerCount < 2)
		{

			Leave();

		}

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
