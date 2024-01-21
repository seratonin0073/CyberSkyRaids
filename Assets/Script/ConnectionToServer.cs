using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using Unity.VisualScripting;

public class ConnectionToServer : MonoBehaviourPunCallbacks
{
    public static ConnectionToServer Instance;
    [SerializeField] private TMP_InputField inputRoomName;
    [SerializeField] private TMP_Text roomName;
    [SerializeField] private Transform transformRoomList;
    [SerializeField] private Transform transformPlayerList;
    [SerializeField] private GameObject roomItemPrefab; 
    [SerializeField] private GameObject playerListItem;
    [Space(10)]
    [SerializeField] private GameObject startGameButton;
    [SerializeField] private TMP_Text text_message;
    private void Awake() 
    {
        Instance = this;
        PhotonNetwork.ConnectUsingSettings();
       
    }
    public override void OnConnectedToMaster()
    {
       PhotonNetwork.JoinLobby();
       PhotonNetwork.AutomaticallySyncScene = true;
    }
    public override void OnJoinedLobby()
    {
        WindowsManager.Layout.OpenLayout("MainMenu");
        Debug.Log("Connected to Lobby!");
        
    }
    public void CreateNewRoom()
    {
        if(string.IsNullOrEmpty(inputRoomName.text))
        {
            return;
        }
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 3
            ;
        
        PhotonNetwork.CreateRoom(inputRoomName.text, roomOptions);

    }
    public override void OnJoinedRoom()
    {
        Player[] players = PhotonNetwork.PlayerList;
        int count = 0;
        for(int i = 0; i < players.Length; i++)
        {

            if (players[i].NickName == PhotonNetwork.NickName)
            {
                count++;
               
            }
            
            if(count > 1)
            {
                  PhotonNetwork.LeaveRoom();
            }

        }

        
        WindowsManager.Layout.OpenLayout("GameRoom");
        if(PhotonNetwork.IsMasterClient) startGameButton.SetActive(true);           
        else startGameButton.SetActive(false);
            
        roomName.text = PhotonNetwork.CurrentRoom.Name;
        
        foreach (Transform trns in transformPlayerList)
        {
            Destroy(trns.gameObject);
        }
        for(int i = 0; i < players.Length; i++)
        {
         Instantiate(playerListItem, transformPlayerList).GetComponent<PlayerListItem>().SetUp(players[i]);   
        }
    }


    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        if(returnCode == 32765)
        {
            WindowsManager.Layout.OpenLayout("Message");
            text_message.text = "This room is full, please find another room.";
         
        }
        else
        {

        }
    }



    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if(PhotonNetwork.IsMasterClient) startGameButton.SetActive(true);           
        else startGameButton.SetActive(false);
    }
    public void ConnectToRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        WindowsManager.Layout.OpenLayout("MainMenu");
   
    }
    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform trns in transformRoomList)
        {
            Destroy(trns.gameObject);
        }
        for(int i = 0; i < roomList.Count; i++)
        {
            Instantiate(roomItemPrefab, transformRoomList).GetComponent<RoomItem>().SetUp(roomList[i]);
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {

        Instantiate(playerListItem, transformPlayerList).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }

    public void StartGameLevel(int levelIndex)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount > 0)
        {

            PhotonNetwork.LoadLevel(levelIndex);

        }
       
    }


    public void Quit_B()
    {
        Application.Quit();
    }


}
