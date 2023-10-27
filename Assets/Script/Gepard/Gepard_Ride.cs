using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gepard_Ride : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;
    [SerializeField] private float speedRotation = 1.5f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 directionForward = Vector3.back;
    [SerializeField] private Vector3 directionBack = Vector3.forward;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }




    void Update()
    {
      /*
        if (Input.GetKey(KeyCode.W))
        {

            transform.Translate(directionForward * Time.deltaTime * speed);

        }
        else if (Input.GetKey(KeyCode.S))
        {

            transform.Translate(directionBack * Time.deltaTime * speed);
        }
      */

        float forw = Input.GetAxis("Vertical");

        transform.Translate(directionForward * Time.deltaTime * speed * forw);

        float h = Input.GetAxis("Horizontal") * speedRotation;

        Quaternion rotate = transform.rotation * Quaternion.Euler(0, h * speedRotation, 0);
        
        transform.rotation = Quaternion.Lerp(transform.rotation, rotate, 15 * Time.deltaTime);
    }
}
