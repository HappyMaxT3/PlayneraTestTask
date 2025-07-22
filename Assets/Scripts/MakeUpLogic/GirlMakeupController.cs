using UnityEngine;

public class GirlMakeupController : MonoBehaviour
{
    [Header("Makeup Layers")]
    public SpriteRenderer faceBaseRenderer; 
    public SpriteRenderer lipsLayerRenderer;
    public SpriteRenderer eyesLayerRenderer; 
    public SpriteRenderer cheeksLayerRenderer;
    public SpriteRenderer pimplesLayerRenderer; 

    [SerializeField] private Sprite initialFaceSprite;

    void Start()
    {
        if (faceBaseRenderer != null && initialFaceSprite != null)
        {
            faceBaseRenderer.sprite = initialFaceSprite;
        }

        if (lipsLayerRenderer != null) lipsLayerRenderer.sprite = null;
        if (eyesLayerRenderer != null) eyesLayerRenderer.sprite = null;
        if (cheeksLayerRenderer != null) cheeksLayerRenderer.sprite = null;
        if (pimplesLayerRenderer != null) pimplesLayerRenderer.gameObject.SetActive(true);
    }

    public void ApplyLipstick(Sprite lipSpriteToApply)
    {
        if (lipsLayerRenderer != null)
        {
            lipsLayerRenderer.sprite = lipSpriteToApply;
        }
    }

    public void ApplyEyeshadow(Sprite eyeSpriteToApply)
    {
        if (eyesLayerRenderer != null)
        {
            eyesLayerRenderer.sprite = eyeSpriteToApply;
        }
    }

    public void RemovePimples()
    {
        if (pimplesLayerRenderer != null)
        {
            pimplesLayerRenderer.gameObject.SetActive(false);
        }
    }

    public void ResetMakeup()
    {
        if (lipsLayerRenderer != null) lipsLayerRenderer.sprite = null;
        if (eyesLayerRenderer != null) eyesLayerRenderer.sprite = null;
        if (cheeksLayerRenderer != null) cheeksLayerRenderer.sprite = null;
        if (pimplesLayerRenderer != null) pimplesLayerRenderer.gameObject.SetActive(true);
    }
}