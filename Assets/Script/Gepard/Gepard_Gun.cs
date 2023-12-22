using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;


public class Gepard_Gun : MonoBehaviour
{
	[SerializeField] private GameObject GunTower;
	[SerializeField] private GameObject Gun;
	[SerializeField] private GameObject Guns;
	[SerializeField] private float speedRotationX = 2.5f;
	[SerializeField] private float speedRotationY = 2.5f;
	[SerializeField] private float RotationInt = 50f;
	[SerializeField] private float CoolDown = 0.5f;
	[SerializeField] private GameObject[] guns;
	[SerializeField] private GameObject bulletPrefab;
	[SerializeField] private bool IsShoot = true;
	[SerializeField] private float minAngle = -5f; //ebool
	[SerializeField] private float maxAngle = 80f; //ebool
	[SerializeField] private ParticleSystem muzzleFire0;
	[SerializeField] private ParticleSystem muzzleFire1;
	[SerializeField] private int Ammo = 4;
	[SerializeField] private int MaxAmmo = 310;
	[SerializeField] private float TimeReload = 8f;
	[SerializeField] private RawImage crosshair;
	[SerializeField] private float currentTimeReload;
	[SerializeField] private bool IsReload = false;
	[SerializeField] private Text ammodis;
	[SerializeField] private float distanceToEnemy;
	[SerializeField] private float angleToEnemy;
	[SerializeField] private RawImage EnemyMark;
	[SerializeField] private RawImage MeMark;
	[SerializeField] private float scaleRadar = 5f;
	[SerializeField] private GameObject guntowerb;
	[SerializeField] public GameObject aaCar;
	[SerializeField] private AudioSource shoot0;
    [SerializeField] private AudioSource reload;
    float y0;




	IEnumerator Shoot()
	{

		IsShoot = false;
		GameObject bullet = Instantiate(bulletPrefab, guns[0].transform.position, Gun.transform.rotation);
		GameObject bullet1 = Instantiate(bulletPrefab, guns[1].transform.position, Gun.transform.rotation);
		muzzleFire0.Play();
		muzzleFire1.Play();
        shoot0.Play();
        bullet.transform.parent = null;
		bullet1.transform.parent = null;
		bullet.GetComponent<Bullet>().TakeForce();
		bullet1.GetComponent<Bullet>().TakeForce();
		Ammo -= 2;
		yield return new WaitForSeconds(CoolDown);
		IsShoot = true;
	}





	private void FindTarget()
	{

		GameObject target = GameObject.FindGameObjectWithTag("Drone");
		if (target != null && EnemyMark != null)
		{

			distanceToEnemy = Vector3.Distance(transform.position, target.transform.position);

			Vector3 dirtar = target.transform.position - transform.position;

			angleToEnemy = Vector3.SignedAngle(dirtar, transform.forward, Vector3.up);




			EnemyMark.rectTransform.localPosition = new Vector3(dirtar.x / scaleRadar, dirtar.z / scaleRadar, EnemyMark.rectTransform.localPosition.z);

			if (EnemyMark.rectTransform.localPosition.x < -412 || EnemyMark.rectTransform.localPosition.x > 420)
			{
				EnemyMark.rectTransform.localPosition = new Vector3(-1082, 495, 0);
			}
			if (EnemyMark.rectTransform.localPosition.y < -217 || EnemyMark.rectTransform.localPosition.y > 218)
			{
				EnemyMark.rectTransform.localPosition = new Vector3(-1082, 495, 0);
			}

		}

		
	}


	void Start()
	{
		/*  GunTower = GameObject.Find("Gepard_Tower");
		  Gun = GameObject.Find("GunPointCenter");

		  Guns = GameObject.Find("Turrets_low");
		 */
		InvokeRepeating("FindTarget", 0, 0.3f);
		Cursor.lockState = CursorLockMode.Locked;
	}


	void Update()
	{	
		
		if (aaCar.GetComponent<Gepard_Ride>().canRide)		
		{

			// Поворот башни

			float x0 = Input.GetAxis("Mouse X") * speedRotationX;

			Quaternion rotate = GunTower.transform.rotation * Quaternion.Euler(0, x0 * speedRotationX, 0);

			GunTower.transform.rotation = Quaternion.Lerp(GunTower.transform.rotation, rotate, RotationInt * Time.deltaTime);







			y0 += Input.GetAxis("Mouse Y") * speedRotationY;

			y0 = Mathf.Clamp(y0, minAngle, maxAngle);

			Gun.transform.localEulerAngles = new Vector3(y0, 0, 0);

			/*
			Quaternion rotatey = Gun.transform.rotation * Quaternion.Euler(y0 * speedRotation, 0, 0);

			float x2 = Mathf.Clamp(rotatey.x, -0.07525f, 0.65066f);

		   // Debug.Log(x2);

		   Debug.Log("In: " + rotatey);



			Debug.Log("Out: " + new Quaternion(x2,rotatey.y,rotatey.z,rotatey.w));


			Gun.transform.rotation = Quaternion.Lerp(Gun.transform.rotation, rotatey, RotationInt * Time.deltaTime);

			*/


			// Стрельба

			if (Input.GetMouseButton(0) && IsShoot && Ammo > 0 && !IsReload)
			{
				StartCoroutine(Shoot());
			}

			if (Input.GetMouseButtonDown(0) && !IsReload && Ammo < 1)
			{
				IsReload = true;

                reload.Play();

                currentTimeReload = Time.time + TimeReload;

			}

			if (Input.GetKeyDown(KeyCode.R) && !IsReload)
			{
				IsReload = true;

                reload.Play();

                currentTimeReload = Time.time + TimeReload;
			}

			ammodis.text = "Ammo: " + Ammo + " / " + MaxAmmo;

			if (Ammo < 0)
			{
				Ammo = 0;
			}

			if (IsReload)
			{

				crosshair.color = Color.red;

				ammodis.text = "Reloading... ";

				

				if (currentTimeReload <= Time.time)
				{
					reload.Stop();
					Ammo = MaxAmmo;
					IsReload = false;
				}

			}
			else
			{
				crosshair.color = Color.green;
			}

		}

	}
}
