using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
    [SerializeField] private float minAngle = -80f; //ebool
    [SerializeField] private float maxAngle = 6.75f; //ebool
    [SerializeField] private ParticleSystem muzzleFire0;
    [SerializeField] private ParticleSystem muzzleFire1;


    float y0;

    void Start()
    {
        GunTower = GameObject.Find("Gepard_Tower");
        Gun = GameObject.Find("GunPointCenter");
        Cursor.lockState = CursorLockMode.Locked;
        Guns = GameObject.Find("Turrets_low");
     
    }


    IEnumerator Shoot()
    {
        IsShoot = false;
        GameObject bullet = Instantiate(bulletPrefab, guns[0].transform.position, Gun.transform.rotation);
        GameObject bullet1 = Instantiate(bulletPrefab, guns[1].transform.position, Gun.transform.rotation);
        muzzleFire0.Play();
        muzzleFire1.Play();
        bullet.transform.parent = null;
        bullet1.transform.parent = null;
        bullet.GetComponent<Bullet>().TakeForce();
        bullet1.GetComponent<Bullet>().TakeForce();
        yield return new WaitForSeconds(CoolDown);
        IsShoot = true;
    }


    void Update()
    {
        // Поворот башни

        float x0 = Input.GetAxis("Mouse X") * speedRotationX;

        Quaternion rotate = GunTower.transform.rotation * Quaternion.Euler(0, x0 * speedRotationX, 0);

        GunTower.transform.rotation = Quaternion.Lerp(GunTower.transform.rotation, rotate, RotationInt * Time.deltaTime);





        y0 += Input.GetAxis("Mouse Y") * speedRotationY * (-1);

        y0 = Mathf.Clamp(y0, minAngle, maxAngle);

        Gun.transform.localEulerAngles = new Vector3(y0,0,0);
        
        /*
        Quaternion rotatey = Gun.transform.rotation * Quaternion.Euler(y0 * speedRotation, 0, 0);

        float x2 = Mathf.Clamp(rotatey.x, -0.07525f, 0.65066f);

       // Debug.Log(x2);
         
       Debug.Log("In: " + rotatey);

        

        Debug.Log("Out: " + new Quaternion(x2,rotatey.y,rotatey.z,rotatey.w));


        Gun.transform.rotation = Quaternion.Lerp(Gun.transform.rotation, rotatey, RotationInt * Time.deltaTime);

        */


        // Стрельба

        if (Input.GetMouseButton(0) && IsShoot)
        {
            StartCoroutine(Shoot());
        }






    }




  
}
