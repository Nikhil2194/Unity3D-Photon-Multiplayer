using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PowerUpSpawnScript : MonoBehaviourPunCallbacks
{
    [SerializeField] public GameObject powerUpPrefab;
    private float spawnInterval= 20f;


    private void Start()
    {
        StartCoroutine(SpawningPowerUp());
    }

    IEnumerator SpawningPowerUp()
    {
        while(true)
        {
            var RandPos = new Vector3(Random.Range(-6f, -60f), 2, Random.Range(70, -56));
           GameObject obj1= PhotonNetwork.Instantiate(powerUpPrefab.name, RandPos, Quaternion.identity);
            obj1.transform.SetParent(gameObject.transform); 

            yield return new WaitForSeconds(spawnInterval);
        } 
    }
}
