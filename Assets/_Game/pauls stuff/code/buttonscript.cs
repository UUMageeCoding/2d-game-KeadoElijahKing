using UnityEngine;
using UnityEngine.EventSystems;

public class buttonscript : MonoBehaviour
{
    public GameObject highlightObject;  // The GameObject to display the sprite on hover
    public Sprite hoverSprite;          // The sprite to display when hovering

    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component

    void Start()
    {
        // Ensure the highlightObject is inactive at the start
        if (highlightObject != null)
        {
            highlightObject.SetActive(false);
            spriteRenderer = highlightObject.GetComponent<SpriteRenderer>();
        }
    }

    // This method is called when the mouse enters the button's collider
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("hoverappear");
        if (highlightObject != null)
        {
            // Activate the highlightObject and set the sprite
            highlightObject.SetActive(true);

            if (spriteRenderer != null && hoverSprite != null)
            {
                spriteRenderer.sprite = hoverSprite;
            }
        }

    }

    // This method is called when the mouse exits the button's collider
    public void OnPointerExit(PointerEventData eventData)
    {
        if (highlightObject != null)
        {
            // Deactivate the highlightObject when hover ends
            highlightObject.SetActive(false);
        }
    }
}

