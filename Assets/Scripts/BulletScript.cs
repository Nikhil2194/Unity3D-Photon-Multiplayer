using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviourPun
{
    public float DestroyTime = 2f;
    public PhotonView pv;
    public float bulletDamage = 0.05f;

    public float destroyTime = 3f;

    //IEnumerator destroyBullet()
    //{
    //    yield return new WaitForSeconds(DestroyTime);
    //    this.GetComponent<PhotonView>().RPC("DestroyBullet", RpcTarget.AllBuffered);
    //}

    private void Start()
    {
        if(photonView.IsMine)
        Invoke("Destroybullets", destroyTime);
    }

    [PunRPC]
    void Destroybullets()
    {
        PhotonNetwork.Destroy(gameObject); // Destroy the bullet over the network
    }


    /*  [PunRPC]
      void DestroyBullet()
      {
          Destroy(this.gameObject);
      }*/


    private void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine)
        {
            return;
        }
        PhotonView target = other.gameObject.GetComponent<PhotonView>();


        if (target != null && ((!target.IsMine) ||target.IsRoomView))
        {
            if (target.tag == "Player")
            {
                target.RPC("HealthUpdate", RpcTarget.AllBuffered, bulletDamage);
            }
         //   this.GetComponent<PhotonView>().RPC("Destroybullets", RpcTarget.AllBuffered);
        }
    }
}
