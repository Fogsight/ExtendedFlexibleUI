using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class FlexibleUIText : FlexibleUI {
    private TextMeshProUGUI textTMP;
    public bool inheritFontSize = true;

    public override void OnSkinUI() {
        base.OnSkinUI();
        textTMP = GetComponent<TextMeshProUGUI>();
        textTMP.font = flexibleUITheme.fontAsset;
        textTMP.fontMaterial = flexibleUITheme.fontMaterial;
        textTMP.enableWordWrapping = flexibleUITheme.textWrap;
        if (inheritFontSize) textTMP.fontSize = flexibleUITheme.fontSize;
    }

    public void SetSizeDelta() {
        GetComponent<RectTransform>().sizeDelta = new Vector2(flexibleUITheme.elementWidth, flexibleUITheme.elementHeight);
    }
}