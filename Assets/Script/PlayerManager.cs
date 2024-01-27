using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    private PhotonView photonView;

    [SerializeField] private GameObject[] DroneSpawn;

    [SerializeField] private GameObject[] AASpawn;

    [SerializeField] private GameObject[] HungarSpawn;

    [SerializeField] private GameObject AA;

    [SerializeField] private GameObject Drone;

    [SerializeField] private GameObject Hungar;

    [SerializeField] private GameObject FreeCamera;

    private int countRound = 0;

    private int scoreAA = 0;

    [SerializeField] private TextMeshProUGUI ScoreA;

    [SerializeField] private TextMeshProUGUI ScoreD;

    [SerializeField] private GameObject UI;


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

        if (!photonView.IsMine)
        {
            Destroy(UI);
        }

   

    }




   



    private void CreateControllerClient()
    {
 
      AA = PhotonNetwork.Instantiate(Path.Combine("sci-Fi-Car"), AASpawn[Random.Range(0, AASpawn.Length-1)].transform.position, Quaternion.identity);
        
        Hungar = PhotonNetwork.Instantiate(Path.Combine("Hungar"), HungarSpawn[Random.Range(0, HungarSpawn.Length - 1)].transform.position, Quaternion.identity);

        
    }



    private void CreateControllerHost()
    {
       Drone = PhotonNetwork.Instantiate(Path.Combine("Sci-fi-Plane (1)"),
           DroneSpawn[Random.Range(0, DroneSpawn.Length - 1)].transform.position, DroneSpawn[Random.Range(0, DroneSpawn.Length - 1)].transform.rotation);
       
    }









    private void FixedUpdate()
    {
        if (photonView.IsMine)
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
}
