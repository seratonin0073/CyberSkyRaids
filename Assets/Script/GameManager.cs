using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{


    public void LeaveRoom()
    {
        // Покидаем текущую комнату
        PhotonNetwork.LeaveRoom();
    }

    // Вызывается при успешном выходе из комнаты
    public override void OnLeftRoom()
    {
        // Загружаем сцену меню (замените "MainMenu" на вашу сцену меню)
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lobbi");
    }






}
