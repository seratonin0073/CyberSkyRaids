using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float Speed = 10f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float TimeLife = 5f;
    private static GameObject obj = null;
    Vector3 previousPosition;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
       
    }

    public static GameObject GetC()
    {
        return obj;
    }

    public float GetSpeed()
    {
        return rb.velocity.magnitude;
    }


    public void TakeForce()
    {

        Vector3 dir = transform.rotation * Vector3.back;

        rb.AddForce(dir * Speed, ForceMode.Impulse);
       

    }



    void Update()
    {
        //   transform.Translate(Vector3.forward * Speed * Time.deltaTime);

        
    }



	private void OnCollisionEnter(UnityEngine.Collision collision)
	{
            Destroy(this.gameObject);
    }
}
