using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    [SerializeField] Rigidbody2D player;

    public float cameraWidth;
    public float cameraHeight;

    public float horizontalMovAmount= 5;
    public float verticalMovAmount = 5;
    public bool fixedCamera = true;

    Vector3 camPos;

    // Start is called before the first frame update
    void Start()
    {
        cameraHeight = Camera.main.orthographicSize * 2;
        cameraWidth = Camera.main.aspect * cameraHeight;
    }

    // Update is called once per frame
    void Update()
    {

        if (fixedCamera)
        {
            Camera.main.transform.position = new Vector3 (player.position.x, player.position.y, -10);
        }
        else
        {
            camPos = Camera.main.transform.position;

            if ((-player.position.x + cameraWidth) < (cameraWidth / 2 - camPos.x))
            {
                Camera.main.transform.position = new Vector3(horizontalMovAmount + camPos.x, camPos.y,
                camPos.z);
            }
            else if ((player.position.x + cameraWidth) < (cameraWidth / 2 + camPos.x))
            {
                Camera.main.transform.position = new Vector3(-horizontalMovAmount + camPos.x, camPos.y,
                camPos.z);
            }
            else if ((-player.position.y + cameraHeight) < (cameraHeight / 2 - camPos.y))
            {
                Camera.main.transform.position = new Vector3(camPos.x, verticalMovAmount + camPos.y, camPos.z);
            }
            else if ((player.position.y + cameraHeight) < (cameraHeight / 2 + camPos.y))
            {
                Camera.main.transform.position = new Vector3(camPos.x, -verticalMovAmount + camPos.y, camPos.z);
            }

            camPos = Camera.main.transform.position;
        }

        if (Input.GetKey("r") && !fixedCamera)
        {
            Camera.main.transform.position = new Vector3(0, 0, -10);
        }
    }
}

