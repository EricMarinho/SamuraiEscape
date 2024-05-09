using UnityEngine;
using NaughtyAttributes;
using UnityEditor;

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
        
        EditorUtility.SetDirty(this);
        EditorUtility.SetDirty(cameraPosData);
        PrefabUtility.RecordPrefabInstancePropertyModifications(this); // IDK if it is needed
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
