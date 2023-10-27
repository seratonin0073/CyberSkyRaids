using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float Speed = 10f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float TimeLife = 5f;
   
    void Start()
    {
        rb = GetComponent<Rigidbody>();

       
    }


    public void TakeForce()
    {

        Vector3 dir = transform.rotation * Vector3.forward;

        rb.AddForce(dir * Speed, ForceMode.Impulse);


    }



    void Update()
    {
     //   transform.Translate(Vector3.forward * Speed * Time.deltaTime);


       

    }




    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Bullet"))
        {
            Destroy(this.gameObject);
        }
    }
}
