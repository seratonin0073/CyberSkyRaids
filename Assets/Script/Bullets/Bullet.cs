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

   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(timerTag());
       
    }

    public static GameObject GetC()
    {
        return obj;
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

    IEnumerator timerTag()
    {
        yield return new WaitForSeconds(TimeLife);
        Destroy(this.gameObject);
    }

}
