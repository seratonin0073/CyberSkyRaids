using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpaceShip : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Dest());
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    IEnumerator Dest()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
