using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class FlexibleUIButton : FlexibleUI {
    private Button button;
    private Image image;

    public override void OnSkinUI() {
        base.OnSkinUI();
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.targetGraphic = image;
        image.type = Image.Type.Sliced;
        image.sprite = flexibleUITheme.buttonSprite;
        image.color = flexibleUITheme.color;
        //Conform text RectTransform sizeDelta to the button if part of a layout
        if (GetComponent<LayoutElement>() != null) GetComponentInChildren<FlexibleUIText>().SetSizeDelta();
    }
}