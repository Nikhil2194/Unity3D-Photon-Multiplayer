using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpeedingScript : MonoBehaviourPun
{
    public float destroyTime = 10f;
    private void Start()
    {
        if (photonView.IsMine)
            Invoke("DestroyPrefab", destroyTime);
    }

    [PunRPC]
    void DestroyPrefab()
    {
        PhotonNetwork.Destroy(gameObject); // Destroy the perfab over the network after some amount of time
    }
}
