using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
using Photon.Pun;

public class FreeCameraScript : MonoBehaviour
{
    [SerializeField] public CinemachineFreeLook[] CFL;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private float Multiply = 3f;
    private PhotonView photonView;
    private float FOV = 40;
    private int count = 0;

    // Start is called before the first frame update

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        Cursor.lockState = CursorLockMode.Locked;
        if (!photonView.IsMine)
        {
            Destroy(playerCamera);
        }

    }



    private void Update()
    {
        if (CFL[0].Follow == null || CFL[0].LookAt == null) return;

        if (CFL[1].Follow == null || CFL[1].LookAt == null) return;


        if (!photonView.IsMine) return;

        FOV += Input.GetAxis("Mouse ScrollWheel") * Multiply * Time.deltaTime;
        FOV = Mathf.Clamp(FOV, 1, 150);

        foreach (var item in CFL)
        {
            item.m_Lens.FieldOfView = FOV;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchCameraForward();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchCameraBack();
        }







    }


    private void SwitchCameraForward()
    {
        CFL[count].gameObject.SetActive(false);
        count++;
        if (count >= CFL.Length)
            count = 0;
        CFL[count].gameObject.SetActive(true);
    }
    private void SwitchCameraBack()
    {
        CFL[count].gameObject.SetActive(false);
        count--;
        if (count < 0)
            count = CFL.Length-1;
        CFL[count].gameObject.SetActive(true);
    }
}
