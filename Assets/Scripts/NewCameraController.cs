using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCameraController : MonoBehaviour
{
    public Vector3 currentCameraPosition;
    [SerializeField] private float lerpSpeed = 5f;

    [SerializeField] private bool isFreeCameraActive = false;

    public static NewCameraController instance;

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
}
