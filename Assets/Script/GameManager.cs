using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{


    public void LeaveRoom()
    {
        // �������� ������� �������
        PhotonNetwork.LeaveRoom();
    }

    // ���������� ��� �������� ������ �� �������
    public override void OnLeftRoom()
    {
        // ��������� ����� ���� (�������� "MainMenu" �� ���� ����� ����)
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lobbi");
    }






}
