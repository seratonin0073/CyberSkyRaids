using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class NetworkStatistic : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text onlineCounterText; 
    void Start()
    {
        InvokeRepeating("UpdateOnlineCounter",2f,1f);
    }
    private void UpdateOnlineCounter()
    {
        onlineCounterText.text = $"Players online: {PhotonNetwork.CountOfPlayersOnMaster}";
    }
}
