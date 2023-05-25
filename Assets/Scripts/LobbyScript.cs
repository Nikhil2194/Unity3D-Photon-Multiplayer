using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LobbyScript : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject loaderImage;
    [SerializeField] GameObject LoginPanel;
    [SerializeField] public TMP_InputField playerNameInput;
   // [SerializeField] public TMP_InputField roomNameInput;

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    void Start()
    {
      //  LoginPanel.SetActive(false);
       // loaderImage.SetActive(true);
    }

    public override void OnConnectedToMaster()
    {
        LoginPanel.SetActive(true);
        loaderImage.SetActive(false);
    }

    public void OnPlayButtonClicked()
    {
        string playerName = playerNameInput.text;
        string roomName = "NIKHIL"; //roomNameInput.text;
        byte maxPlayers;
        maxPlayers = 20;

        RoomOptions options = new RoomOptions { MaxPlayers = maxPlayers, PlayerTtl = 10000 };
        PhotonNetwork.JoinOrCreateRoom(roomName, options, null);
        PhotonNetwork.LocalPlayer.NickName = playerName;
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("DemoPlay");
    }
}
