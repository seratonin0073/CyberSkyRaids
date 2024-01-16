using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PhotonView photonView;

    [SerializeField] private GameObject DroneSpawn;

    [SerializeField] private GameObject AASpawn;

    bool clients = false;

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {

        if (photonView.IsMine)
        {
            Debug.Log(photonView.Owner.IsMasterClient);
           
            if (photonView.Owner.IsMasterClient)
            {
                CreateControllerHost();
                

            }
            else if (!photonView.Owner.IsMasterClient && PhotonNetwork.NickName != "FreeCam")
            {
                CreateControllerClient();
                clients = true;
            }else if (PhotonNetwork.NickName == "FreeCam")
            {
                CreateControllerFreeCamera();
            }

            


            
        }
       
    }

    private void CreateControllerClient()
    {
        PhotonNetwork.Instantiate(Path.Combine("sci-Fi-Car"),
            AASpawn.transform.position, Quaternion.identity);
    }

    private void CreateControllerHost()
    {
        PhotonNetwork.Instantiate(Path.Combine("Sci-fi-Plane (1)"),
           DroneSpawn.transform.position, Quaternion.identity);
    }

    private void CreateControllerFreeCamera()
    {
        PhotonNetwork.Instantiate(Path.Combine("FreeCamera"),
           Vector3.zero, Quaternion.identity);
    }
}
