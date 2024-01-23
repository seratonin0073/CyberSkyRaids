using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO; // дозволяє працювати із файлами(копіювати,переміщувати,відкривати і т.д)

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;
    private void Awake()
    {
    

        

     

        if(GameObject.FindGameObjectsWithTag("xren").Length > 1)
        {
            Destroy(GameObject.FindGameObjectsWithTag("xren")[1]);
        }

        if (Instance != null && Instance == GetComponent<RoomManager>())
        {
           
            Destroy(GameObject.FindGameObjectsWithTag("xren")[0]);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }
    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        
        if(scene.buildIndex >= 1)
        {
            PhotonNetwork.Instantiate(Path.Combine("PlayerManager"), Vector3.zero, Quaternion.identity);
        }
    }

}
