using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class HealthScript : MonoBehaviourPun
{
    [SerializeField] public Image fillImage;
    [SerializeField] public float heath=1;
    [SerializeField] public ParticleSystem bloodEffect;
   // [SerializeField] public ParticleSystem boomEffect;
    [SerializeField] public GameObject playerPrefab;


    public void CheckHealth()
    {
        if(photonView.IsMine && heath<=0)
        {
            this.GetComponent<PhotonView>().RPC("PlayerDeath", RpcTarget.AllBuffered);
        }
    }
    [PunRPC]
    public void HealthUpdate(float _damage)
    {
        fillImage.fillAmount -= _damage;
        bloodEffect.Play();
        heath = fillImage.fillAmount;
        CheckHealth();
    }


    [PunRPC]
    public void PlayerDeath()
    {
        //boomEffect.Play();
        playerPrefab.SetActive(false);
        StartCoroutine(EnablePlayer());
    }


    IEnumerator EnablePlayer()
    {
        yield return new WaitForSeconds(5f);  // after 5sec below codw will execute
        playerPrefab.SetActive(true);
        fillImage.fillAmount = 1;
    }
}