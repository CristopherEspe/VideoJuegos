using UnityEngine;
using UnityEngine.UI;

public class MainMenuBackground : MonoBehaviour
{
    public Image backgroundImage;

    void Start()
    {
        if (backgroundImage != null)
        {
            AdjustBackgroundSize();
        }
    }

    void AdjustBackgroundSize()
    {
        RectTransform rectTransform = backgroundImage.rectTransform;
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float imageAspect = backgroundImage.sprite.bounds.size.x / backgroundImage.sprite.bounds.size.y;

        if (screenAspect >= imageAspect)
        {
            float scaleHeight = screenAspect / imageAspect;
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.offsetMin = new Vector2(0, -(scaleHeight - 1) * rectTransform.rect.height / 2);
            rectTransform.offsetMax = new Vector2(0, (scaleHeight - 1) * rectTransform.rect.height / 2);
        }
        else
        {
            float scaleWidth = imageAspect / screenAspect;
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.offsetMin = new Vector2(-(scaleWidth - 1) * rectTransform.rect.width / 2, 0);
            rectTransform.offsetMax = new Vector2((scaleWidth - 1) * rectTransform.rect.width / 2, 0);
        }
    }
}
