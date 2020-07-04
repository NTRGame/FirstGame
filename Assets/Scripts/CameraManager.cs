using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    private float speed = 1f;
    private Camera mainCamera;
    private float size;
    private Vector3 position;
    private GameObject info,head;


    void Start()
    {
        mainCamera = GetComponent<Camera>();
        size = mainCamera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                transform.position = transform.position + new Vector3(-Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"), 0) * speed;
            }
        }

        if(Input.GetAxis("Mouse ScrollWheel") > 0 && mainCamera.orthographicSize>5)
        {
            mainCamera.orthographicSize -= speed;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && mainCamera.orthographicSize < 15 )
        {
            mainCamera.orthographicSize += speed;
        }

        //if (Input.mousePosition.x > Screen.width * 0.98)
        //{
        //    transform.position = transform.position + new Vector3(speed, 0, 0);
        //}
        //if (Input.mousePosition.y > Screen.height * 0.98)
        //{
        //    transform.position = transform.position + new Vector3(0, speed, 0);
        //}
        //if (Input.mousePosition.x < Screen.width * 0.03)
        //{
        //    transform.position = transform.position - new Vector3(speed, 0, 0);
        //}
        //if (Input.mousePosition.y < Screen.height * 0.03)
        //{
        //    transform.position = transform.position - new Vector3(0, speed, 0);
        //}
    }
}
