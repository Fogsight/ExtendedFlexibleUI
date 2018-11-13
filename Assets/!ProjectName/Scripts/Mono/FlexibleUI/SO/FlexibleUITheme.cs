using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Custom/UI/FlexibleUITheme")]
public class FlexibleUITheme : GenericCollection<Object> {
    public Sprite buttonSprite;
    public Color color;
    public TMP_FontAsset fontAsset;
    public Material fontMaterial;
    public bool textWrap = true;
    public float fontSize;
    public float scaleUI = 1f;
    [Header("Layout Settings")]
    public float elementWidth;
    public float elementHeight;

    [Header("Slider (sprites are in order of appearence)")]
    public Sprite[] sprites;

    private static PopRef popRef;

    public void OnValidate() {
        //Upon switching item list, move everything to new one
        int count = Items.Count - 1;
        for (int i = count; i >= 0; i--) {
            //take out leftover empty items
            if (Items[i] == null) {
                Remove(Items[i]);
                continue;
            }
            FlexibleUI flexibleUI = Items[i] as FlexibleUI;
            LayoutElement layoutElement = flexibleUI.GetComponent<LayoutElement>();
            if (layoutElement != null) {
                layoutElement.minWidth = layoutElement.preferredWidth = elementWidth;
                layoutElement.minHeight = layoutElement.preferredHeight = elementHeight;
            }
            flexibleUI.OnSkinUI();
        }
    }

    private void OnEnable() {
        if (popRef == null) popRef = FindObjectOfType<PopRef>(); //fails to find during start up
        if (popRef != null && !popRef.allThemes.Contains(this)) popRef.allThemes.Add(this);
    }

    private void OnDisable() {
        if (popRef != null && popRef.allThemes.Contains(this)) popRef.allThemes.Remove(this);
    }
}