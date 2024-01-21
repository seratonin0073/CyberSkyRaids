using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceshipController : MonoBehaviourPunCallbacks
{
	public bool pressingThrottle = false;
	public bool throttle => pressingThrottle;

	private bool canBoom = true;

    [SerializeField]
    private float
    flySpeed = 50f,
    tangazhSpeed = 0.1f;

    private float horizontalMovement;
    private float Amount = 120;
    [SerializeField] private GameObject BoomObj;
	[SerializeField] private ParticleSystem partSyst;
	[SerializeField] private ParticleSystem[] fire;
	[SerializeField] private Rigidbody rb;
	[SerializeField] private Collider colliders;
	[SerializeField] private GameObject plane;
	[SerializeField] private GameObject targetObject;
	[SerializeField] private float S_DTT;
	[SerializeField] private float T;
	[SerializeField] private float V = 20;
	[SerializeField] private GameObject EP;
	[SerializeField] private AudioSource audio1;
	[SerializeField] private AudioSource audio2;
	[SerializeField] private AudioSource fire_sound;
	[SerializeField] private Light[] lights;
	

    [SerializeField] private PhotonView photonView; // ����, ������� ������ � ������� - ����    :D

    [SerializeField] private GameObject PauseMeny;

    Rigidbody boomRB;

	private bool canFly;

	private float activeRoll, activePitch, activeYaw;

	private void Start()
	{
		if (!photonView.IsMine)
		{
			Destroy(plane);
		}

		if (plane != null)
		{
			plane.SetActive(false);
		}
        audio1.Play();   
		boomRB = BoomObj.AddComponent<Rigidbody>();
		canFly = true;
		rb.useGravity = false;
		boomRB.useGravity = false;
		boomRB.freezeRotation = false;
		boomRB.constraints = RigidbodyConstraints.None;



		
    }


    public void SetTargetPoint(float time1)
    {
        T = time1;
        S_DTT = T * V - 30;


        Vector3 targetPosition = transform.position - targetObject.transform.up * S_DTT * Random.Range(0.8f, 1.2f);
       

        targetObject.transform.position = targetPosition;


    }



    public GameObject TargetPointGet() { return targetObject; }


    private void OnCollisionEnter(Collision collision)
    {
		Debug.Log("wdwdwdwdwdwdwdwwwdwdwwdw: " + collision.gameObject + " " + canBoom);
		if(collision.gameObject.name != "SpaceShipBullet(Clone)")
        {


        if (canBoom)
        {

		  if (plane != null)  plane.SetActive(true);
		  
			

        boomRB.freezeRotation = true;
		boomRB.constraints = RigidbodyConstraints.FreezePosition;
		Debug.Log(collision.gameObject);
		canFly = false;
		pressingThrottle = false;
		rb.useGravity = true;
        audio1.Stop();
        StartCoroutine(timerTag(2));
       
            foreach (var f in fire)
            {
                f.Play();
            }
			foreach(var l in lights)
			{
				Destroy(l.gameObject);
			}
            rb.AddForce(collision.transform.position, ForceMode.Impulse);
			partSyst.Play();
			audio2.Play();
            fire_sound.Play();
            canBoom = false;

         
                photonView.RPC(nameof(NotifyCollision1), RpcTarget.All, collision.transform.position);

				Debug.Log("RPC");

		
			


        }
        }

	}


    [Photon.Pun.PunRPC]
    private void NotifyCollision1(Vector3 col)
    {
		Debug.Log("wef");
        if (canBoom)
        {

            if (plane != null) plane.SetActive(true);



            boomRB.freezeRotation = true;
            boomRB.constraints = RigidbodyConstraints.FreezePosition;
            canFly = false;
            pressingThrottle = false;
            rb.useGravity = true;
            audio1.Stop();
            StartCoroutine(timerTag(2));

            foreach (var f in fire)
            {
                f.Play();
            }
            foreach (var l in lights)
            {
                Destroy(l.gameObject);
            }
            rb.AddForce(col, ForceMode.Impulse);
            partSyst.Play();
            audio2.Play();
            fire_sound.Play();
            canBoom = false;
          


        }


    }


 


    IEnumerator timerTag(float duration)
	{
		yield return new WaitForSeconds(duration);
        transform.gameObject.tag = "Untagged";
    }



    public override void OnLeftRoom()
    {
       
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



    private void FixedUpdate()
	{
        if (!photonView.IsMine)
        {
            return;
        }

		
		if (Input.GetKey(KeyCode.Escape))
		{

            OpenM();

        }

        if (PhotonNetwork.CurrentRoom.PlayerCount < 1)
        {

            Leave();

        }






        if (canFly)
		{

            transform.Translate(Vector3.forward * flySpeed * Time.fixedDeltaTime);

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            horizontalMovement += horizontal * Amount * Time.fixedDeltaTime;
            float verticalMovement = Mathf.Lerp(0, 30, Mathf.Abs(vertical)) * Mathf.Sign(vertical);
            float roll = Mathf.Lerp(0, 40, Mathf.Abs(horizontal)) * -Mathf.Sign(horizontal);
            Debug.Log(Mathf.Sign(10));

            transform.localRotation = Quaternion.Euler(Vector3.up * horizontalMovement + Vector3.right * verticalMovement + Vector3.forward * roll);
        }
		else{
			if (plane != null)
			{
				plane.SetActive(true);
			}
		}
	}
}