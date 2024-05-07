using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(BoxCollider2D))]
public class CameraTriggerController : MonoBehaviour
{
    [SerializeField] private CameraPosData cameraPosData;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            NewCameraController.instance.SetCameraPosition(cameraPosData.cameraPos);
            Debug.Log("Camera Position Set to" + cameraPosData.cameraPos);
        }
    }

    [Button("Set Camera Position")]
    public void SetCameraPosition()
    {
        cameraPosData.cameraPos = Camera.main.transform.position;
    }
}
