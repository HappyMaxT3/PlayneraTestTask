using UnityEngine;

public class ColorSwatch : MonoBehaviour
{
    public Sprite appliedSprite;
    public DraggableCosmeticItem.CosmeticType cosmeticType;

    [Header("Highlight Settings")]
    public SpriteRenderer swatchRenderer;
    public Color highlightColor = Color.yellow;
    private Color originalColor;

    void Awake()
    {
        if (swatchRenderer == null)
        {
            swatchRenderer = GetComponent<SpriteRenderer>();
        }
        if (swatchRenderer != null)
        {
            originalColor = swatchRenderer.color;
        }
    }

    public void SelectSwatch()
    {
        if (swatchRenderer != null)
        {
            swatchRenderer.color = highlightColor;
        }
    }

    public void DeselectSwatch()
    {
        if (swatchRenderer != null)
        {
            swatchRenderer.color = originalColor;
        }
    }
}