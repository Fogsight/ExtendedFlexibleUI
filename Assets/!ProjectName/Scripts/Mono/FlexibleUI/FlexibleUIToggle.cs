using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class FlexibleUIToggle : FlexibleUI {
    private Image[] images;
    private TextMeshProUGUI textTMP;
    public bool inheritFontSize = false;

    public override void OnSkinUI() {
        images = GetComponentsInChildren<Image>();
        base.OnSkinUI();
        if (images.Length == 0) return;//Fixes instantiation order errors, prevents early launch

        //Checkmark background
        images[0].sprite = flexibleUITheme.sprites[1];
        images[0].color = flexibleUITheme.color;
        //Checkmark
        images[1].color = flexibleUITheme.color;

        textTMP = GetComponentInChildren<TextMeshProUGUI>();
        textTMP.font = flexibleUITheme.fontAsset;
        textTMP.fontMaterial = flexibleUITheme.fontMaterial;
        textTMP.enableWordWrapping = flexibleUITheme.textWrap;
        if (inheritFontSize) textTMP.fontSize = flexibleUITheme.fontSize;
    }

    public void SetSizeDelta() {
        GetComponent<RectTransform>().sizeDelta = new Vector2(flexibleUITheme.elementWidth, flexibleUITheme.elementHeight);
    }
}