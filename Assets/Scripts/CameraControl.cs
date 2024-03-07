using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CameraControl : MonoBehaviour
{

    [SerializeField] Rigidbody2D player;

    public float cameraWidth;
    public float cameraHeight;

    public float horizontalMovAmount;
    public float verticalMovAmount;
    
    public float cameraMovSpeed;
    private Vector3 destination;

    public float minCameraDistPlayer;

    public bool fixedCamera;
    public bool fixedCameraWindow;
    public bool lerp;

    Vector3 camPos;

    // Start is called before the first frame update
    void Start()
    {


        fixedCamera = false;
        fixedCameraWindow = true;
        lerp = false;
        cameraMovSpeed = 5f;
        minCameraDistPlayer = 1;
        
        cameraHeight = Camera.main.orthographicSize * 2;
        cameraWidth = Camera.main.aspect * cameraHeight;
        camPos = Camera.main.transform.position;
        destination = camPos;
        horizontalMovAmount = cameraWidth;
        verticalMovAmount = cameraHeight;
    }

    void moveCamera()
    {
        if (Math.Abs(destination.x - camPos.x) > minCameraDistPlayer || Math.Abs(destination.y - camPos.y) > minCameraDistPlayer)
        {
            Camera.main.transform.Translate(
                    (destination.x - camPos.x) * Time.deltaTime * cameraMovSpeed,
                (destination.y - camPos.y) * Time.deltaTime * cameraMovSpeed,
                0);
        }
        else
        {
            //Camera.main.transform.position = camPos;
            //Debug.LogError("PAROU");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (Input.GetKey("r"))
        //{
        //    player.position = new Vector3 (0, player.position.y, 0);
        //    Camera.main.transform.position = new Vector3 (0, 0, -10);
        //}

        camPos = Camera.main.transform.position;

        if (fixedCamera)
        {
            lerp = false;
            fixedCameraWindow = false;
        }
       
        if (fixedCameraWindow) 
        {
            lerp = false;
            fixedCamera = false;
        }
       
        if (lerp)
        {
            fixedCamera = false;
            fixedCameraWindow = false;
        }
        if (fixedCamera)
        {    
            Camera.main.transform.position = new Vector3(player.position.x, player.position.y + cameraHeight / 4, -10);
        }
        else if (fixedCameraWindow)
        {
            if ((player.position.x - camPos.x) > (cameraWidth / 4))
            {
                Camera.main.transform.position = new Vector3(player.position.x - cameraWidth / 4, Camera.main.transform.position.y, -10);
            }
            else if ((player.position.x - camPos.x) < (-cameraWidth / 4))
            {
                Camera.main.transform.position = new Vector3(player.position.x + cameraWidth / 4, Camera.main.transform.position.y, -10);
            }

            if ((player.position.y - camPos.y) > (cameraHeight / 4))
            {
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, player.position.y - cameraHeight / 4, -10);
            }
            else if ((player.position.y - camPos.y) < (-cameraHeight / 4))
            {
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, player.position.y + cameraHeight / 4, -10);
            }
            // PARA CONTROLAR NO FUTURO COM LERP

            //if ((player.position.y - camPos.y) > (cameraHeight / 4))
            //{
            //    Camera.main.transform.Translate(camPos.x, (camPos.y + cameraHeight / 4) * Time.deltaTime, 0);
            //    Debug.LogError("UP " + (camPos.y + cameraHeight / 4));
            //}
            //else if ((player.position.y - camPos.y) < (-cameraHeight / 4))
            //{
            //    Camera.main.transform.Translate(camPos.x, (camPos.y - cameraHeight / 4) * Time.deltaTime, 0);
            //    Debug.LogError("DOWN " + (camPos.y - cameraHeight / 4));
            //}
        }
        else
        {
            if ((-player.position.x + cameraWidth) < (cameraWidth / 2 - camPos.x))
            {
                if (!lerp)
                {
                    Camera.main.transform.position = new Vector3(horizontalMovAmount + camPos.x, camPos.y, camPos.z);
                }
                else
                {
                    destination = new Vector3(horizontalMovAmount + camPos.x, camPos.y, -10);
                    moveCamera();
                }
            }
            else if ((player.position.x + cameraWidth) < (cameraWidth / 2 + camPos.x))
            {
                if (!lerp)
                {
                    Camera.main.transform.position = new Vector3(-horizontalMovAmount + camPos.x, camPos.y, camPos.z);
                }
                else
                {
                    destination = new Vector3(-horizontalMovAmount + camPos.x, camPos.y, -10);
                    moveCamera();
                }
            }

            if ((-player.position.y + cameraHeight) < (cameraHeight / 2 - camPos.y))
            {
                if (!lerp)
                {
                    Camera.main.transform.position = new Vector3(camPos.x, verticalMovAmount + camPos.y, -10);
                }
                else
                {
                    destination = new Vector3(camPos.x, verticalMovAmount + camPos.y, -10);
                    moveCamera();
                }
            }
            else if ((player.position.y + cameraHeight) < (cameraHeight / 2 + camPos.y))
            {
                if (!lerp)
                {
                    Camera.main.transform.position = new Vector3(camPos.x, -verticalMovAmount + camPos.y, -10);
                }
                else
                {
                    destination = new Vector3(camPos.x, -verticalMovAmount + camPos.y, -10);
                    moveCamera();
                }
            }

            if (!fixedCamera && !fixedCameraWindow && lerp) moveCamera();

            camPos = Camera.main.transform.position;

        }
    }
}

