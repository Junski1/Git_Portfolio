using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    float mouseY;
    float mouseX;
    [SerializeField] Transform player;
    [SerializeField] GameObject playerCamera;


    float xRotation = 0f;

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X")*mouseSensitivity*Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        player.Rotate(Vector3.up*mouseX);
    }
}
