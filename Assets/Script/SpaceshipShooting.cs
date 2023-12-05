using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpaceshipShooting : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject bulletSpawnPoint;
    [SerializeField] private float bulletForce;
    [SerializeField] private float YUp;
    [SerializeField] private Camera cam;
    [SerializeField] private bool isReloading = false;
    [SerializeField] private GameObject Parent;
    Vector3 screenSpaceCenter = new Vector3(0.5f, 0.5f, 0);
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 laserEnd = cam.ViewportToWorldPoint(screenSpaceCenter);
        if (Input.GetMouseButtonDown(0))
        {
            if(!isReloading)
            {
                float x = Screen.width / 2;
                float y = Screen.height / 2;

                var ray = cam.ScreenPointToRay(new Vector3(x, y + YUp, 0));
                GameObject bulletClone = GameObject.Instantiate(bullet,bulletSpawnPoint.transform);
                bulletClone.transform.parent = null;
                Rigidbody rb = bulletClone.AddComponent<Rigidbody>();
                rb.useGravity = false;
                rb.velocity = ray.direction * bulletForce;
                //rb.AddForce(screenSpaceCenter * bulletForce,ForceMode.Impulse);
            }
            
        }
    }
}
