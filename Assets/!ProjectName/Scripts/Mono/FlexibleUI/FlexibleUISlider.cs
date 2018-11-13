using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class FlexibleUISlider : FlexibleUI {
    private Image[] images;

    public override void OnSkinUI() {
        base.OnSkinUI();
        images = GetComponentsInChildren<Image>();
        for (int i = 0; i < images.Length; i++) {
            images[i].sprite = flexibleUITheme.sprites[i];
            images[i].color = flexibleUITheme.color;
        }
    }
}