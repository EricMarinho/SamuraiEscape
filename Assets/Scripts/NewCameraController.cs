using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCameraController : MonoBehaviour
{
    public Vector3 currentCameraPosition;
    [SerializeField] private float lerpSpeed = 5f;

    [SerializeField] private bool isFreeCameraActive = false;

    public static NewCameraController instance;
    private Camera cam;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        cam = Camera.main;
        ForceAspectRatio(1.0f);
    }

    private void Update()
    {
        //Move camera with lerp to the currentCameraPosition
        if (isFreeCameraActive)
            return;

        transform.position = Vector3.Lerp(transform.position, currentCameraPosition, Time.deltaTime * lerpSpeed);
    }

    public void SetCameraPosition(Vector3 position)
    {
        currentCameraPosition = position;
    }

    private void ForceAspectRatio(float targetAspect)
    {
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            Rect rect = cam.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
            cam.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = cam.rect;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
            cam.rect = rect;
        }
    }

}
