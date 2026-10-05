using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] int playerSpeed;
    [SerializeField] int forceJump;
    Vector3 movementDir;
    bool isMoving = false;
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (isMoving)
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
        if (jumpContext.performed)
        {
            rb.AddForce(transform.up * forceJump, ForceMode.Impulse);
        }
    }
}
