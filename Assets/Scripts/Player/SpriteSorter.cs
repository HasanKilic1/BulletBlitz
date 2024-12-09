using UnityEngine;

public class SpriteSorter : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        spriteRenderer.sortingOrder = -(int)(transform.position.y * 10);                    
    }
}
