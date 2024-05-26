using UnityEngine;
using NaughtyAttributes;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(BoxCollider2D))]
public class CameraTriggerController : MonoBehaviour
{
    [SerializeField] private CameraPosData cameraPosData;
    [SerializeField] private bool isTeleportCameraTrigger = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            NewCameraController.instance.SetCameraPosition(cameraPosData.cameraPos);
            Debug.Log("Camera Position Set to" + cameraPosData.cameraPos);

            if (isTeleportCameraTrigger)
            {
                PlayerController.instance.SetPlayerPosition(cameraPosData.playerTeleportPosition);
                Debug.Log("Player Position Set to" + cameraPosData.playerTeleportPosition);
            }
        }
    }

#if UNITY_EDITOR
    [Button("Set Camera Position")]
    public void SetCameraPosition()
    {
        cameraPosData.cameraPos = Camera.main.transform.position;
        cameraPosData.playerTeleportPosition = PlayerController.instance.rb.position;
        
        EditorUtility.SetDirty(this);
        EditorUtility.SetDirty(cameraPosData);
        PrefabUtility.RecordPrefabInstancePropertyModifications(this); // IDK if it is needed
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
#endif
}
