using UnityEngine;

public class CameraController : MonoBehaviour
{    
    [SerializeField] Transform player;
    [SerializeField] private Vector2 minMaxXY;
    [SerializeField] private float xMin , xMax , yMin , yMax ;
    private void LateUpdate()
    {
        Vector3 targetPosition = player.position;
        targetPosition.z = -10;
        
        targetPosition.x = Mathf.Clamp(targetPosition.x, xMin, xMax);
        targetPosition.y = Mathf.Clamp(targetPosition.y, yMin, yMax);

        transform.position = targetPosition;    
    }
}
