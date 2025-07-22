using UnityEngine;

public class ColorSwatchSelector : MonoBehaviour
{
    public ColorSwatch colorSwatch;
    public BrushController brushController; 

    void Start()
    {
        if (colorSwatch == null)
        {
            colorSwatch = GetComponent<ColorSwatch>();
        }
    }

    void OnMouseDown()
    {
        if (colorSwatch != null && brushController != null)
        {
            brushController.SelectColor(colorSwatch.appliedSprite, colorSwatch.cosmeticType);

            colorSwatch.SelectSwatch();
        }
    }
}