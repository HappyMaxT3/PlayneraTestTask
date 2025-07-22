using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrushController : MonoBehaviour
{
    [Header("Brush Settings")]
    public Sprite defaultBrushSprite;
    public float returnDuration = 0.3f; 

    [Header("References")]
    public GirlMakeupController girlMakeupController;
    public Collider2D blushApplyZone;
    public Collider2D eyelashApplyZone;
    public List<ColorSwatch> allColorSwatches; 

    private Vector3 originalPosition;
    private bool isDragging = false;
    private SpriteRenderer brushRenderer;
    private Collider2D brushCollider;
    private int originalSortingOrder;

    private Sprite currentlySelectedAppliedSprite;
    private DraggableCosmeticItem.CosmeticType currentlySelectedCosmeticType;

    void Start()
    {
        originalPosition = transform.position;
        brushRenderer = GetComponent<SpriteRenderer>();
        brushCollider = GetComponent<Collider2D>();

        if (brushRenderer != null)
        {
            originalSortingOrder = brushRenderer.sortingOrder;
            brushRenderer.sprite = defaultBrushSprite;
        }
    }

    void OnMouseDown()
    {
        if (isDragging) return;

        isDragging = true;
        if (brushCollider != null) brushCollider.enabled = false;

        if (brushRenderer != null)
        {
            brushRenderer.sortingOrder = 100;
        }
    }

    void OnMouseDrag()
    {
        if (!isDragging) return;

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
    }

    void OnMouseUp()
    {
        if (!isDragging) return;

        isDragging = false;
        if (brushCollider != null) brushCollider.enabled = true;

        if (brushRenderer != null)
        {
            brushRenderer.sortingOrder = originalSortingOrder; 
        }

        if (currentlySelectedAppliedSprite != null)
        {
            if (blushApplyZone != null && blushApplyZone.OverlapPoint(transform.position) && currentlySelectedCosmeticType == DraggableCosmeticItem.CosmeticType.Blush)
            {
                girlMakeupController.ApplyBlush(currentlySelectedAppliedSprite);
            }
            else if (eyelashApplyZone != null && eyelashApplyZone.OverlapPoint(transform.position) && currentlySelectedCosmeticType == DraggableCosmeticItem.CosmeticType.Eyeshadow)
            {
                girlMakeupController.ApplyEyelashes(currentlySelectedAppliedSprite);
            }
        }

        StartCoroutine(ReturnBrushToOriginalPosition());
    }

    public void SelectColor(Sprite appliedSpriteForFace, DraggableCosmeticItem.CosmeticType cosmeticType)
    {
        foreach (ColorSwatch swatch in allColorSwatches)
        {
            if (swatch != null)
            {
                swatch.DeselectSwatch();
            }
        }

        currentlySelectedAppliedSprite = appliedSpriteForFace;
        currentlySelectedCosmeticType = cosmeticType;
    }

    private IEnumerator ReturnBrushToOriginalPosition()
    {
        float elapsed = 0f;
        Vector3 currentPos = transform.position;

        while (elapsed < returnDuration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / returnDuration;
            transform.position = Vector3.Lerp(currentPos, originalPosition, progress);
            yield return null;
        }
        transform.position = originalPosition;
    }
}