using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class PlayerMovementScript : MonoBehaviourPunCallbacks
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float jumpSpeed = 8f;
    [SerializeField] private float gravity = 20f;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject crossAim;
    [SerializeField] private TMP_Text playerName3D;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] Transform BulletSpawnPoint;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] TrailRenderer trail;
  //  [SerializeField] ParticleSystem bulletShootEffect;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;



    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        playerName3D.text = photonView.Owner.NickName;
        trail = GetComponent<TrailRenderer>();
        trail.enabled = false;

        if (photonView.IsMine)
        {
            playerCamera.gameObject.SetActive(true);
            crossAim.gameObject.SetActive(true);
        }
        else
        {
            playerCamera.gameObject.SetActive(false);
            crossAim.gameObject.SetActive(false);
        }
    }



    private void Update()
    {
        if (photonView.IsMine)
        {
            // Camera movement
            float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * 100f;
            float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * 100f;

            transform.Rotate(Vector3.up, mouseX);

          //  float xRotation = playerCamera.transform.rotation.eulerAngles.x - mouseY;

            //xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            playerCamera.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f);

            // Player movement
            if (characterController.isGrounded)
            {
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= movementSpeed;

                if (Input.GetButton("Jump"))
                {
                    moveDirection.y = jumpSpeed;
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                BulletSHoot();
            }

            moveDirection.y -= gravity * Time.deltaTime;
            characterController.Move(moveDirection * Time.deltaTime);
        }
    }

    public void BulletSHoot()
    {
       // bulletShootEffect.Play();
        GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, new Vector3(BulletSpawnPoint.position.x, BulletSpawnPoint.position.y, BulletSpawnPoint.position.z), Quaternion.identity, 0);
        // GameObject bullet = Instantiate(bulletPrefab, new Vector3(BulletSpawnPoint.position.x, BulletSpawnPoint.position.y, BulletSpawnPoint.position.z), Quaternion.identity);
        Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
        bulletRB.AddForce(BulletSpawnPoint.forward * bulletSpeed, ForceMode.Impulse);
    }


    IEnumerator SpeedingCourotuone()
    {
        movementSpeed = 40f;
        trail.enabled = true;
        yield return new WaitForSeconds(9f);
        movementSpeed = 10f;
        trail.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Speeding"))
        {
            Destroy(other.gameObject);
            StartCoroutine(SpeedingCourotuone());
        }
    }
}

