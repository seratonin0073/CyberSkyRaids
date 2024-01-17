using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;

public class RoomItem : MonoBehaviour
{
    [SerializeField] private TMP_Text roomName;
    RoomInfo info;

    public void SetUp(RoomInfo info)
    {
        this.info = info;
        roomName.text = info.Name;
    }
    public void OnClick()
    {
        ConnectionToServer.Instance.JoinRoom(this.info);
    }

}
