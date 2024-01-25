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
    [SerializeField] private float speed_a = 2f;
    private float speed_a1 = 2f;
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

    [SerializeField] private AudioListener al;

    [SerializeField] private GameObject PanelUI;

    void Start()
	{
        
        if (!photonView1.IsMine)
        {
            Destroy(plane);
        }
        if (photonView1.IsMine)
        {
            AudioListener.volume = 1;
        }
        engine1.Play();
        canRide = true;
		rb = GetComponent<Rigidbody>();
		plane.SetActive(false);
        PanelUI.SetActive(true);



    }

	private void OnCollisionEnter(Collision collision)
	{
        

       

        if ((collision.transform.CompareTag("Drone") || collision.transform.CompareTag("Bullet") || collision.transform.name == "Acid") && collision.transform.name != "Bullet(Clone)")
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

                photonView1.RPC(nameof(NotifyCollision), RpcTarget.All);

                if (photonView1.IsMine)
                {
                    AudioListener.volume = 0;
                }

                canRide = false;
                

                StartCoroutine(WaitBeforeRestart());
            }
		}
	}

	IEnumerator WaitBeforeRestart()
	{
		yield return new WaitForSeconds(15);
        /*
		PhotonNetwork.LoadLevel(1);
        engine1.Play();
        expl.Stop();
        explosion.Stop();
        fire_sound.Stop();
        foreach (var f in fire)
        {
            f.Stop();
        }
        canRide = true;
        */



        if(photonView1.IsMine) PhotonNetwork.Destroy(this.gameObject);







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

            if (photonView1.IsMine)
            {
                AudioListener.volume = 0;
            }

            StartCoroutine(WaitBeforeRestart());
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


        if (Input.GetKey(KeyCode.Escape)) OpenM();

        if (PhotonNetwork.CurrentRoom.PlayerCount < 1) Leave();
  
        if (canRide)
		{
			float forw = Input.GetAxis("Vertical_Gepard");
            if (Input.GetKey(KeyCode.LeftShift)) speed_a1 = speed_a;
            else speed_a1 = 1f;

            transform.Translate(directionForward * Time.deltaTime * speed * forw * speed_a1);
			float h = Input.GetAxis("Horizontal_Gepard") * speedRotation;
			Quaternion rotate = transform.rotation * Quaternion.Euler(0, h * speedRotation, 0);
			transform.rotation = Quaternion.Lerp(transform.rotation, rotate, 15 * Time.deltaTime);
			plane.SetActive(false);
		}
		else
		{
			plane.SetActive(true);
            PanelUI.SetActive(false);
        }
	}
}
