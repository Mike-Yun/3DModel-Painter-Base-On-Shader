using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float fastRadio = 5.0f;
    public float rotateSpeed = 1.0f;
    public Camera controlCamera = null;
    // Start is called before the first frame update
    void Start()
    {
        if (null == controlCamera)
        {
            controlCamera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Transform cameraTS = controlCamera.transform;

        //位置控制
        Vector3 position = cameraTS.localPosition;
        float speed = moveSpeed * Time.deltaTime;
        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed *= fastRadio;
        }
        if(Input.GetKey(KeyCode.W))
        {
            position += cameraTS.forward * moveSpeed;
        }
        if(Input.GetKey(KeyCode.S))
        {
            position -= cameraTS.forward * moveSpeed;
        }
        if(Input.GetKey(KeyCode.D))
        {
            position += cameraTS.right * moveSpeed;
        }
        if(Input.GetKey(KeyCode.A))
        {
            position -= cameraTS.right * moveSpeed;
        }
        cameraTS.localPosition = position;

        //朝向控制
        if(Input.GetMouseButton(1))
        {
            Vector3 angles = cameraTS.rotation.eulerAngles;
            angles.x += Input.GetAxis("Mouse Y") * -rotateSpeed;
            angles.y += Input.GetAxis("Mouse X") * rotateSpeed;
            Quaternion rotation = Quaternion.identity;
            rotation.eulerAngles = new Vector3(angles.x, angles.y, 0);
            cameraTS.rotation = rotation;
        }
    }
}
