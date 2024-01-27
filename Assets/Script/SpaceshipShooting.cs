using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Photon.Pun;

public class SpaceshipShooting : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject bulletSpawnPoint;
    [SerializeField] private float bulletForce;
    [SerializeField] private float YUp;
    [SerializeField] private Camera cam;
    [SerializeField] private bool isReloading = false;
    [SerializeField] private GameObject Parent;
    [SerializeField] private AudioSource shoot1;

    [SerializeField] private PhotonView photonView;

    [SerializeField] private GameObject d;

    
    Vector3 screenSpaceCenter = new Vector3(0.5f, 0.5f, 0);




    // Update is called once per frame
    void Update()
    {

        if (!photonView.IsMine || !d.GetComponent<SpaceshipController>().canBoom)
        {
            return;
        }


        Vector3 laserEnd = cam.ViewportToWorldPoint(screenSpaceCenter);
        if (Input.GetMouseButtonDown(0))
        {
            if(!isReloading)
            {
                float x = Screen.width / 2;
                float y = Screen.height / 2;

                var ray = cam.ScreenPointToRay(new Vector3(x, y + YUp, 0));
                GameObject bulletClone = PhotonNetwork.Instantiate("SpaceShipBullet", bulletSpawnPoint.transform.position, Quaternion.identity);
                bulletClone.transform.parent = null;
                Rigidbody rb = bulletClone.AddComponent<Rigidbody>();
                rb.useGravity = false;
                rb.mass = 1;
                rb.velocity = ray.direction * bulletForce;
                rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
                shoot1.Play();
                //rb.AddForce(screenSpaceCenter * bulletForce,ForceMode.Impulse);
            }
            
        }
    }
}
