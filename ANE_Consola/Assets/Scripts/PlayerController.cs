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
            transform.position += movementDir * playerSpeed * Time.deltaTime;
        }
    }
    public void Movement(InputAction.CallbackContext movementContext)
    {
        if (movementContext.performed)
        {
            Vector2 dir = movementContext.ReadValue<Vector2>();
            movementDir = new Vector3(dir.x,0,dir.y);
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
