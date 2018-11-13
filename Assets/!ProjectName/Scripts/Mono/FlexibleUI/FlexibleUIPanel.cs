using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FlexibleUIPanel : FlexibleUI {
    private Image image;

    public override void OnSkinUI() {
        base.OnSkinUI();
        image = GetComponent<Image>();
        image.type = Image.Type.Sliced;
        image.sprite = flexibleUITheme.buttonSprite;
        image.color = flexibleUITheme.color;
    }
}