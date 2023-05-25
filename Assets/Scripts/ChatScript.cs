using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class ChatScript : MonoBehaviour
{
    public TMP_InputField inputFiled;
    public GameObject messageObj;
    public GameObject content;
    public GameObject ChatPanel;
    public GameObject ChatButton;

   public void SendMessage()
    {
        GetComponent<PhotonView>().RPC("GetMessage", RpcTarget.All, PhotonNetwork.NickName+ " : "+ inputFiled.text);
        inputFiled.text = "";
    }

    public void ChatButtonPressed()
    {
        ChatPanel.SetActive(true);
        ChatButton.SetActive(false);
    }

    public void HideChatPanel()
    {
        ChatButton.SetActive(true);
        ChatPanel.SetActive(false);
    }

    [PunRPC]
    public void GetMessage(string _recieveMessage)
    {
       GameObject obj= Instantiate(messageObj, Vector3.zero, Quaternion.identity, content.transform);
        obj.GetComponent<MessageScript>().myMessage.text = _recieveMessage;    //Accessing DaTa from MessageScript
    }
}
