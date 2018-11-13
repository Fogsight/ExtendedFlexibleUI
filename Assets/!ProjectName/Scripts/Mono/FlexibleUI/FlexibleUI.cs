using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public abstract class FlexibleUI : MonoBehaviour {
    public FlexibleUITheme flexibleUITheme;
    public static PopRef popRef;
    public bool applyScale = true;

    private void OnEnable() {
        if (popRef == null) popRef = FindObjectOfType<PopRef>();
        if (flexibleUITheme == null) flexibleUITheme = popRef.themeSwap.allFlexibleUIThemes[popRef.themeSwap.activeIndex];
        flexibleUITheme.Add(this);
        OnSkinUI();
    }

    private void OnDisable() {
        flexibleUITheme.Remove(this);
    }

    public virtual void OnSkinUI() {
        LayoutElement layoutElement = GetComponent<LayoutElement>();
        if (layoutElement != null) {
            UpdateLayout(layoutElement, flexibleUITheme.elementWidth, flexibleUITheme.elementHeight);
        }
        if (applyScale && transform.parent != null && transform.parent.GetComponent<FlexibleUI>() == null) transform.localScale = new Vector2(flexibleUITheme.scaleUI, flexibleUITheme.scaleUI);
    }

    private void OnValidate() {
        OnSkinUI();
    }

    public void AddLayoutElement() {
        if (GetComponent<LayoutElement>() || GetComponent<LayoutGroup>()) return;
        LayoutElement layoutElement = gameObject.AddComponent<LayoutElement>();
        UpdateLayout(layoutElement, flexibleUITheme.elementWidth, flexibleUITheme.elementHeight);
    }

    private void UpdateLayout(LayoutElement layoutElement, float width, float height) {
        layoutElement.preferredWidth = layoutElement.minWidth = width;
        layoutElement.preferredHeight = layoutElement.minHeight = height;
    }
}