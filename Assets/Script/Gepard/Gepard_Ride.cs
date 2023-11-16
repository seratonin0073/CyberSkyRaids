using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gepard_Ride : MonoBehaviour
{
 
    private float horizontal;
    private float vertical;
      [SerializeField] private float speed = 6f;
      [SerializeField] private float speedRotation = 1.5f;
    [SerializeField] private Rigidbody rb;
       private Vector3 directionForward = new Vector3(-1,0,0);
      private Vector3 directionBack = Vector3.forward;


    void Start()
    {
        rb = GetComponent<Rigidbody>();


    }




    void Update()
    {
        



          float forw = Input.GetAxis("Vertical");

          transform.Translate(directionForward * Time.deltaTime * speed * forw);
         
  



        float h = Input.GetAxis("Horizontal") * speedRotation;

        Quaternion rotate = transform.rotation * Quaternion.Euler(0, h * speedRotation, 0);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotate, 15 * Time.deltaTime);




    }

}
