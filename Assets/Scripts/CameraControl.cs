using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CameraControl : MonoBehaviour
{

    [SerializeField] Rigidbody2D player;
    [SerializeField] GameObject cinematicStart;
    [SerializeField] GameObject cinematicEnd;

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
    enum cinematicEnum
    {
        cam2cam,
        cam2player,
        end
    }

    cinematicEnum cinematicRun;
    
    public float cinematicSpeed1;
    public float cinematicSpeed2;

    public float cinematicScaleStep;
    public float cinematicScaledOut;
    private Vector3 cinematicVector;

    // Start is called before the first frame update
    void Start()
    {        
        Rect rect = Camera.main.rect;

        rect.width = 1;
        rect.height = 1;
        rect.x = 0;
        rect.y = 0;

        Camera.main.rect = rect;

        cinematicRun = (int) cinematicEnum.cam2cam;
        cinematicSpeed1 = 0.25f;
        cinematicSpeed2 = 0.5f;
        cinematicScaleStep = 0.1f;
        cinematicScaledOut = 15;

        fixedCamera = true;
        fixedCameraWindow = false;
        lerp = false;
        cameraMovSpeed = 5f;
        minCameraDistPlayer = 1;

        cameraHeight = Camera.main.orthographicSize * 2;
        cameraWidth = Camera.main.aspect * cameraHeight;
        camPos = Camera.main.transform.position;
        destination = camPos;
        horizontalMovAmount = cameraWidth;
        verticalMovAmount = cameraHeight;

        cinematicRun = cinematicEnum.cam2cam;
        cinematicSpeed1 = 0.25f;
        cinematicSpeed2 = 1f;
        cinematicScaleStep = 0.1f;
        cinematicScaledOut = 10;
        resetCamera();

    }

    void resetCamera()
    {
        Camera.main.transform.position = cinematicStart.transform.position;
        Camera.main.orthographicSize = cinematicScaledOut;
        cinematicRun = cinematicEnum.cam2cam;

        cinematicVector = cinematicEnd.transform.position - Camera.main.transform.position;
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

        if (Input.GetKey("r")) resetCamera();

        if (cinematicRun != cinematicEnum.end)
        {
            if (cinematicRun == cinematicEnum.cam2cam)
            {
                Camera.main.transform.Translate(cinematicVector.x * Time.deltaTime * cinematicSpeed1,
                                            cinematicVector.y * Time.deltaTime * cinematicSpeed1, 0);

                if (Math.Abs(Camera.main.transform.position.x - cinematicEnd.transform.position.x) < 0.1 &&
                    Math.Abs(Camera.main.transform.position.y - cinematicEnd.transform.position.y) < 0.1)
                {
                    cinematicRun = cinematicEnum.cam2player;
                    cinematicVector = player.transform.position - Camera.main.transform.position;

                }
            }
            else 
            {
                Camera.main.transform.Translate(cinematicVector.x * Time.deltaTime * cinematicSpeed2,
                                              cinematicVector.y * Time.deltaTime * cinematicSpeed2, 0);

                Camera.main.orthographicSize = Math.Max(5, Camera.main.orthographicSize - cinematicScaleStep);


                if (Math.Abs(Camera.main.transform.position.x - player.transform.position.x) < 0.1 &&
                    Math.Abs(Camera.main.transform.position.y - player.transform.position.y) < 0.1) cinematicRun = cinematicEnum.end;
            }
           
        }
        else
        {

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
                //Camera.main.transform.position = new Vector3(player.position.x, player.position.y + cameraHeight / 4, -10);
                Camera.main.transform.position = new Vector3(player.position.x, player.position.y, -10);
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
}

