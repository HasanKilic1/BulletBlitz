using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    private Vector2 movementVector;
    void Start()
    {        
    }

    void Update()
    {
        
    }

    public void SetMovementVector(CallbackContext callbackContext)
    {
        Debug.Log("Movement vector : " + callbackContext.ReadValue<Vector2>());
    }
}
