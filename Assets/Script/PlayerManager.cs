using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    private PhotonView photonView;

    [SerializeField] private GameObject DroneSpawn;

    [SerializeField] private GameObject AASpawn;

    [SerializeField] private GameObject AA;

    [SerializeField] private GameObject Drone;


    bool clients = false;

    bool isStart = false;

  

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            
           
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

        isStart = true;


    }




   



    private void CreateControllerClient()
    {
      AA = PhotonNetwork.Instantiate(Path.Combine("sci-Fi-Car"), AASpawn.transform.position, Quaternion.identity);
      

     

    }

 

    private void CreateControllerHost()
    {
       Drone = PhotonNetwork.Instantiate(Path.Combine("Sci-fi-Plane (1)"),
           DroneSpawn.transform.position, DroneSpawn.transform.rotation);
    }

    private void CreateControllerFreeCamera()
    {
        PhotonNetwork.Instantiate(Path.Combine("FreeCamera"),
           Vector3.zero, Quaternion.identity);
    }



    private void FixedUpdate()
    {




        if (photonView.IsMine)
        {
            if (!photonView.Owner.IsMasterClient && AA == null && PhotonNetwork.NickName != "FreeCam")
            {
                AA = PhotonNetwork.Instantiate(Path.Combine("sci-Fi-Car"), AASpawn.transform.position, Quaternion.identity);
            }
            if (photonView.Owner.IsMasterClient && Drone == null)
            {
                Drone = PhotonNetwork.Instantiate(Path.Combine("Sci-fi-Plane (1)"),
                 DroneSpawn.transform.position, DroneSpawn.transform.rotation);
            }

        }



    }

}
