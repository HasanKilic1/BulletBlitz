using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player;
    
    private void LateUpdate()
    {
        Vector3 targetPosition = player.position;
        targetPosition.z = -10;

        transform.position = targetPosition;    
    }
}
