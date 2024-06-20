using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float mouseSensitivity = 100f;

    float xRotation = 0f;
    float YRotation = 0f;


    void Start()
    {
        // default visibility in middle screen window
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // control right or left vision
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -30f, 30f);

        // control up or down vision
        YRotation += mouseX;

        // applying all rotations
        transform.localRotation = Quaternion.Euler(xRotation, YRotation, 0f);


    }
}
