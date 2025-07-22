using UnityEngine;
using System.Collections;

public class DraggableCosmeticItem : MonoBehaviour
{
    [Header("Item Settings")]
    public CosmeticType cosmeticType;
    public Sprite appliedSprite; // Для губки может быть null

    [Header("References")]
    public GirlMakeupController girlMakeupController;
    public Collider2D applyZoneCollider; // Зона нанесения (например, Collider2D для LipsZone, PimpleApplyZone, или новой зоны для губки)

    private Vector3 originalPosition;
    private bool isDragging = false;
    private Collider2D itemCollider;
    private SpriteRenderer itemSpriteRenderer;
    private int originalSortingOrder;

    public enum CosmeticType { Lipstick, Eyeshadow, Blush, Cream, Sponge }

    void Start()
    {
        originalPosition = transform.position;
        itemCollider = GetComponent<Collider2D>();
        itemSpriteRenderer = GetComponent<SpriteRenderer>();
        if (itemSpriteRenderer != null)
        {
            originalSortingOrder = itemSpriteRenderer.sortingOrder;
        }
    }

    void OnMouseDown()
    {
        if (isDragging) return;

        isDragging = true;
        if (itemCollider != null) itemCollider.enabled = false;

        if (itemSpriteRenderer != null)
        {
            itemSpriteRenderer.sortingOrder = 100;
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

        if (itemCollider != null) itemCollider.enabled = true;

        if (itemSpriteRenderer != null)
        {
            itemSpriteRenderer.sortingOrder = originalSortingOrder;
        }

        if (applyZoneCollider.OverlapPoint(transform.position))
        {
            switch (cosmeticType)
            {
                case CosmeticType.Lipstick:
                    girlMakeupController.ApplyLipstick(appliedSprite);
                    break;
                case CosmeticType.Eyeshadow:
                    girlMakeupController.ApplyEyeshadow(appliedSprite);
                    break;
                case CosmeticType.Blush:
                    girlMakeupController.ApplyBlush(appliedSprite);
                    break;
                case CosmeticType.Cream:
                    girlMakeupController.RemovePimples();
                    break;
                case CosmeticType.Sponge:
                    girlMakeupController.ResetMakeup();
                    break;
            }
            StartCoroutine(ReturnToOriginalPosition());
        }
        else
        {
            StartCoroutine(ReturnToOriginalPosition());
        }
    }

    private IEnumerator ReturnToOriginalPosition()
    {
        float duration = 0.3f;
        float elapsed = 0f;
        Vector3 currentPos = transform.position;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / duration;
            transform.position = Vector3.Lerp(currentPos, originalPosition, progress);
            yield return null;
        }
        transform.position = originalPosition;
    }
}