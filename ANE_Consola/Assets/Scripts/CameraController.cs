using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] int cameraSpeed;
    [SerializeField] Transform playerTransform;
    Vector3 movementDir;
    bool ismovingCamera = false;
    InvokePauseMenu pauseMenu;

    private void Start()
    {
        pauseMenu = InvokePauseMenu.instance;
    }

    private void LateUpdate()
    {
        if (ismovingCamera && !pauseMenu.IsGamePaused())
        {
            playerTransform.rotation = 
                Quaternion.Euler(playerTransform.rotation.eulerAngles + new Vector3(0, movementDir.x * cameraSpeed * Time.deltaTime, 0));
            this.transform.rotation = 
                Quaternion.Euler(this.transform.rotation.eulerAngles + new Vector3(-movementDir.y * cameraSpeed * Time.deltaTime, 0, 0));
        }
    }
    public void MoveCamera(InputAction.CallbackContext moveCameraContext)
    {
        movementDir = moveCameraContext.ReadValue<Vector2>();
        if (moveCameraContext.performed)
        {
            ismovingCamera = true;
        }
        else if (moveCameraContext.canceled)
        {
            movementDir = Vector2.zero;
            ismovingCamera = false;
        }
    }
}
