using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    private Vector2 movementVector;

    void FixedUpdate()
    {        
        rb.linearVelocity = moveSpeed * Time.deltaTime * movementVector.normalized;
    }

    public void OnMovementInput(InputAction.CallbackContext callbackContext)
    {
        movementVector = callbackContext.ReadValue<Vector2>();
    }
}
