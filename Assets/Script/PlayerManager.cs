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
            else if (!photonView.Owner.IsMasterClient && PhotonNetwork.NickName != "FreeCam")
            {
                CreateControllerClient();
                clients = true;
            }else if (PhotonNetwork.NickName == "FreeCam")
            {
                CreateControllerFreeCamera();
            }

            


            
        }

        if (!photonView.IsMine)
        {
            Destroy(UI);
        }

        Debug.Log("Started!");

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

    private void CreateControllerFreeCamera()
    {
        FreeCamera = PhotonNetwork.Instantiate(Path.Combine("FreeCamera"),
           Vector3.zero, Quaternion.identity);
    }


    private void Start()
    {

        if (FreeCamera == null && !photonView.IsMine) return;

        AA = GameObject.FindGameObjectWithTag("tank");

        Drone = GameObject.FindGameObjectWithTag("Drone");

        Debug.Log(GameObject.FindGameObjectWithTag("Drone"));

        FreeCamera.gameObject.GetComponent<FreeCameraScript>().CFL[0].Follow = Drone.transform;
        FreeCamera.gameObject.GetComponent<FreeCameraScript>().CFL[0].LookAt = Drone.transform;
        FreeCamera.gameObject.GetComponent<FreeCameraScript>().CFL[1].Follow = AA.GetComponent<Gepard_Ride>().GetMDL().transform;
        FreeCamera.gameObject.GetComponent<FreeCameraScript>().CFL[1].LookAt = AA.GetComponent<Gepard_Ride>().GetMDL().transform;


    }





    private void FixedUpdate()
    {
        return;



        if (photonView.IsMine)
        {
            if (!photonView.Owner.IsMasterClient && AA == null && PhotonNetwork.NickName != "FreeCam")
            {
                AA = PhotonNetwork.Instantiate(Path.Combine("sci-Fi-Car"), AASpawn[Random.Range(0, AASpawn.Length - 1)].transform.position, Quaternion.identity);
            
     

            }
            if (photonView.Owner.IsMasterClient && Drone == null)
            {
                Drone = PhotonNetwork.Instantiate(Path.Combine("Sci-fi-Plane (1)"),
                 DroneSpawn[Random.Range(0, DroneSpawn.Length - 1)].transform.position, DroneSpawn[Random.Range(0, DroneSpawn.Length - 1)].transform.rotation);
       
            
               

               

            }

            if (Hungar == null && (!photonView.Owner.IsMasterClient && PhotonNetwork.NickName != "FreeCam"))
            {
         

               

            }

           

         



        }





    }





}
