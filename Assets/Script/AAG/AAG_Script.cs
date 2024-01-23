using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AAG_Script : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject Muzzle;
    [SerializeField] private GameObject targetDrone;    
    [SerializeField] private Transform target;
    [SerializeField] private float range;
    [SerializeField] private float RotateSpeed = 3f;
    [SerializeField] private float CoolDown = 1f;
    [SerializeField] private bool IsShoot = true;
    [SerializeField] private bool IsShootCD = false;
    [SerializeField] private GameObject[] guns;
    [SerializeField] private ParticleSystem muzzleFire0;
    [SerializeField] private ParticleSystem muzzleFire1;
    [SerializeField] private ParticleSystem Expl;
    [SerializeField] private ParticleSystem[] fireEff;
    [SerializeField] public Rigidbody drone0;
    [SerializeField] public float bullet_speed;
    [SerializeField] public float drone_speed;
    [SerializeField] public float ATD_S;
    [SerializeField] public float S1;
    [SerializeField] public float T;
    [SerializeField] private AudioSource fire;
    [SerializeField] private AudioSource boom1;
    [SerializeField] private AudioSource fire1;
    private bool isDestroyed = false;
    GameObject TargetDrone;
    [SerializeField] private PhotonView photonView1;



    private void OnDrawGizmos()
    {

        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, range);

    }

    void Start()
    {
        // bullet_speed = bulletPrefab.GetComponent<Bullet>().GetSpeed();
        drone_speed = 50;
        bullet_speed = 615;

        InvokeRepeating("FindTarget", 0f, 0.3f);
     

    }




    void Update()
    {
        if (!isDestroyed)
        {
            Look();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

    

        if (collision.transform.CompareTag("Drone") || collision.transform.CompareTag("Bullet") && !isDestroyed && collision.transform.name != "Bullet(Clone)")
        {
                boom1.Play();
                fire.Play();
                Expl.Play();
                fire1.Play();
                foreach (var f in fireEff)
                {
                    f.Play();
                }
                isDestroyed = true;
                photonView1.RPC(nameof(NotifyCollision), RpcTarget.All);

        }
    }

    [Photon.Pun.PunRPC]
    private void NotifyCollision()
    {
  
        

        boom1.Play();
        fire.Play();
        Expl.Play();
        fire1.Play();
        foreach (var f in fireEff)
        {
            f.Play();
        }
        isDestroyed = true;


    }



        IEnumerator Shoot()
    {
        

            IsShoot = false;
            if (IsShootCD)
            {
            GameObject bullet = PhotonNetwork.Instantiate("Bullet", guns[0].transform.position, transform.rotation * Quaternion.Euler(0, 90, 0));
            GameObject bullet1 = PhotonNetwork.Instantiate("Bullet", guns[1].transform.position, transform.rotation * Quaternion.Euler(0, 90, 0));
            muzzleFire0.Play();
            muzzleFire1.Play();
            fire.Play();
            bullet.transform.parent = null;
            bullet1.transform.parent = null;
            bullet.GetComponent<Bullet>().TakeForce();
            bullet1.GetComponent<Bullet>().TakeForce();
            }
            yield return new WaitForSeconds(CoolDown);
            IsShoot = true;
        
    }


    private void FindTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Drone");
        GameObject CurrentTarget = null;
       
        float distance = Mathf.Infinity;

        foreach (GameObject target in targets)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, target.transform.position);

            if (distanceToEnemy < distance)
            {

                distance = distanceToEnemy;
                CurrentTarget = target;
                ATD_S = distance;
                T = ATD_S / bullet_speed;
                
                targetDrone = CurrentTarget.GetComponent<SpaceshipController>().TargetPointGet();
                CurrentTarget.GetComponent<SpaceshipController>().SetTargetPoint(T);

                IsShootCD = true;


            }

        }

        if (distance <= range && CurrentTarget != null)
        {

            this.target = targetDrone.transform;
           
        }

        else
        {
            this.target = null;
            this.targetDrone = null;
            IsShootCD = false;
        }


    }


    private void Look()
    {



        if (target != null)
        {
            
            Quaternion look = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, look * Quaternion.Euler(0, 90, 0), Time.deltaTime + RotateSpeed);

            if (IsShoot)
            {
                
                StartCoroutine(Shoot());

            }

        }
        


    }

}
