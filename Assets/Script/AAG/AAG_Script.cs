using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AAG_Script : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject Muzzle;
    [SerializeField] private Transform target;
    [SerializeField] private float range;
    [SerializeField] private float RotateSpeed = 3f;
    [SerializeField] private float CoolDown = 5f;
    [SerializeField] private bool IsShoot = false;
    [SerializeField] private bool IsShootCD = false;
    [SerializeField] private GameObject[] guns;
    [SerializeField] private ParticleSystem muzzleFire0;
    [SerializeField] private ParticleSystem muzzleFire1;

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, range);

    }

    void Start()
    {
        InvokeRepeating("FindTarget", 0f, 0.3f);
     

    }




    void Update()
    {
       
        Look();
    }


    IEnumerator Shoot()
    {
        IsShoot = false;
        GameObject bullet = Instantiate(bulletPrefab, guns[0].transform.position, transform.rotation * Quaternion.Euler(0, -270, 0));
        GameObject bullet1 = Instantiate(bulletPrefab, guns[1].transform.position, transform.rotation * Quaternion.Euler(0, -270, 0));
        muzzleFire0.Play();
        muzzleFire1.Play();
        bullet.transform.parent = null;
        bullet1.transform.parent = null;
        bullet.GetComponent<Bullet>().TakeForce();
        bullet1.GetComponent<Bullet>().TakeForce();
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
                IsShoot = true;
               

            }

        }

        if (distance <= range && CurrentTarget != null)
        {

            this.target = CurrentTarget.transform;

        }

        else
        {
            this.target = null;
            IsShoot = false;
        }


    }


    private void Look()
    {



        if (target != null)
        {
            IsShootCD = true;
            Quaternion look = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, look * Quaternion.Euler(0, 90, 0), Time.deltaTime + RotateSpeed);

            if (IsShoot)
            {

                StartCoroutine(Shoot());

            }

        }
        


    }

}
