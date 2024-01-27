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

<<<<<<< Updated upstream
=======
    [SerializeField] private GameObject Hungar;

    [SerializeField] private GameObject FreeCamera;

    [SerializeField] public static bool isEnd = false;

    [SerializeField] public static bool isWin = false; // Anti-aircraft

    [SerializeField] private TextMeshProUGUI ScoreA;

    [SerializeField] private TextMeshProUGUI ScoreD;

    [SerializeField] private GameObject UI;

    [SerializeField] private GameObject prefWin;

    [SerializeField] private GameObject prefLose;

>>>>>>> Stashed changes

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
            else if (!photonView.Owner.IsMasterClient)
            {
                CreateControllerClient();
               
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
<<<<<<< Updated upstream




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

=======
        if (photonView.IsMine && !isEnd)
        {






            if (!photonView.Owner.IsMasterClient && AA == null && PhotonNetwork.NickName != "FreeCam")
            {
                AA = PhotonNetwork.Instantiate(Path.Combine("sci-Fi-Car"), AASpawn[Random.Range(0, AASpawn.Length - 1)].transform.position, Quaternion.identity);

            }
            if (photonView.Owner.IsMasterClient && Drone == null)
            {

                Leave();

            }

            if (Hungar == null && (!photonView.Owner.IsMasterClient && PhotonNetwork.NickName != "FreeCam"))
            {

                Leave();

            }


        }

    }



    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Lobbi");

        Cursor.lockState = CursorLockMode.None;
    }

>>>>>>> Stashed changes
}
