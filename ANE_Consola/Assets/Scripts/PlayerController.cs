using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] int playerSpeed;
    [SerializeField] int forceJump;
    [SerializeField]LayerMask groundLayerMask;
    Vector3 movementDir;
    bool isMoving = false;
    Rigidbody rb;

    InvokePauseMenu pauseMenu;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pauseMenu = InvokePauseMenu.instance;
    }
    private void Update()
    {
        if (isMoving && !pauseMenu.IsGamePaused())
        {
            transform.position += movementDir.normalized * playerSpeed * Time.deltaTime;
        }
    }
    public void Movement(InputAction.CallbackContext movementContext)
    {
        if (movementContext.performed)
        {
            Vector2 dir = movementContext.ReadValue<Vector2>();
            Vector3 _movementDir = new Vector3(dir.x, 0, dir.y);

            movementDir = _movementDir.x * transform.right + _movementDir.z * transform.forward;

            isMoving = true;
        }
        else if (movementContext.canceled)
        {
            movementDir = Vector2.zero;
            isMoving = false;
        }
    }
    public void Jump(InputAction.CallbackContext jumpContext)
    {
        if (jumpContext.performed && !pauseMenu.IsGamePaused() && Physics.Raycast(this.transform.position, -transform.up, 1.2f, groundLayerMask))
        {
            rb.AddForce(transform.up.normalized * forceJump, ForceMode.Impulse);
        }
    }
}
