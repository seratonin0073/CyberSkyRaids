using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungarScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] Flame;

    [SerializeField] private ParticleSystem Exp; // Explotion

    [SerializeField] private AudioSource boom;

    [SerializeField] private AudioSource fire;

    [SerializeField] private bool isDestroy = false;

    [SerializeField] private PhotonView photonView1;

    private void Awake()
    {

        foreach (ParticleSystem item in Flame) item.Stop();

        Exp.Stop();

        boom.Stop();
            
        fire.Stop();

        isDestroy = false;
    }

    void Boom()
    {
        if (isDestroy) return;

        foreach (ParticleSystem item in Flame) item.Play();

        Exp.Play();

        boom.Play();

        fire.Play();

        isDestroy = true;



    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag("Drone")) return;


        Boom();

        photonView1.RPC(nameof(BoomRPC), RpcTarget.All);

        StartCoroutine(dest());


    }

    [Photon.Pun.PunRPC]

    public void BoomRPC()
    {
        Boom();
        StartCoroutine(dest());
    }

    IEnumerator dest()
    {
        yield return new WaitForSeconds(10);

        Destroy(gameObject);
    
    }



}
