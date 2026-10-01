using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] int cameraSpeed;
    Vector3 movementDir;
    bool ismovingCamera = false;

    public void MoveCamera(InputAction.CallbackContext moveCameraContext)
    {
        
    }
}
