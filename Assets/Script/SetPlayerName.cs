using UnityEngine;
using Photon.Pun;
using TMPro;
public class SetPlayerName : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField nickNameField;
    public override void OnConnectedToMaster()
    {
        LoadNickName();
    }
    public void ChangeName()
    {
        PlayerPrefs.SetString("SaveNickName", nickNameField.text);
        LoadNickName();
    }
    private void LoadNickName()
    {
        // Беремо із комірки пам'яті "SaveNickName" ім'я гравця 
        string playerName = PlayerPrefs.GetString("SaveNickName");
        if(string.IsNullOrEmpty(playerName))
        {
            playerName = "Player" + Random.Range(0,1000);           
        }
        PhotonNetwork.NickName = playerName;
        nickNameField.text = playerName;
    }
}
