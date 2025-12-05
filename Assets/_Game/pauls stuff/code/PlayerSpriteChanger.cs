using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerSpriteChanger : MonoBehaviour
{
    public Sprite newSprite;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) ;
    }
   /* private void OnCollisionEnter2D(Collision2D collision)
    {
        ChangeSprite();
        
    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("changeSprite"))
        {
            ChangeSprite();
            Debug.Log("SpriteSwap");
        }
        
    }
    void ChangeSprite()
    {
        if (newSprite != null)
        {
            spriteRenderer.sprite = newSprite;
        }
    }
}
