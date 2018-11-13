using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public abstract class FlexibleUIInstance : Editor {
    public static PopRef popRef;

    [MenuItem("GameObject/Flexible UI/Button", priority = 0)]
    public static void AddButton() {
        GameObject buttonGO = Create<FlexibleUIButton>("Button");
        GameObject textGO = Create<FlexibleUIText>("Text", buttonGO);
        TextMeshProUGUI text = textGO.GetComponent<TextMeshProUGUI>();
        text.text = "Button";
        text.alignment = TextAlignmentOptions.Center;
        PopRefCheck();
        text.font = popRef.themeSwap.allFlexibleUIThemes[popRef.themeSwap.activeIndex].fontAsset;
    }

    [MenuItem("GameObject/Flexible UI/Panel", priority = 0)]
    public static void AddPanel() {
        Create<FlexibleUIPanel>("Panel");
    }

    [MenuItem("GameObject/Flexible UI/Slider", priority = 0)]
    public static void AddSlider() {
        var targetT = Selection.activeTransform;
        EditorApplication.ExecuteMenuItem("GameObject/UI/Slider");
        var slider = Selection.activeGameObject;
        slider.transform.SetParent(targetT, true);
        slider.AddComponent<FlexibleUISlider>();
    }

    [MenuItem("GameObject/Flexible UI/Toggle", priority = 0)]
    public static void AddToggle() {
        FlexibleUIToggle flexibleUIToggle = Create<FlexibleUIToggle>("Toggle").GetComponent<FlexibleUIToggle>();
        Image backgroundImage = Create<Image>("Background", flexibleUIToggle.gameObject).GetComponent<Image>();
        Image checkmarkImage = Create<Image>("Checkmark", backgroundImage.gameObject).GetComponent<Image>();
        TextMeshProUGUI label = Create<TextMeshProUGUI>("Label", flexibleUIToggle.gameObject).GetComponent<TextMeshProUGUI>();
        PopRefCheck();
        RectTransform rt = flexibleUIToggle.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(160f, 20f);
        backgroundImage.sprite = popRef.themeSwap.allFlexibleUIThemes[popRef.themeSwap.activeIndex].sprites[1];
        checkmarkImage.sprite = popRef.themeSwap.allFlexibleUIThemes[popRef.themeSwap.activeIndex].sprites[3];

        rt = backgroundImage.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0, 1f);
        rt.anchorMax = new Vector2(0, 1f);
        rt.localPosition = new Vector2(-70f, 0);
        rt.sizeDelta = new Vector2(20f, 20f);

        rt = checkmarkImage.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(20f, 20f);

        Toggle toggle = flexibleUIToggle.GetComponent<Toggle>();
        toggle.graphic = checkmarkImage;
        toggle.targetGraphic = backgroundImage;
        toggle.isOn = true;

        rt = label.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(1, 1);
        rt.localPosition = new Vector2(10f, 0);
        rt.sizeDelta = new Vector2(-26f, -4f);
        label.font = popRef.themeSwap.allFlexibleUIThemes[popRef.themeSwap.activeIndex].fontAsset;
        label.fontSize = 16f;
        label.text = "Label";
        flexibleUIToggle.OnSkinUI();
    }

    [MenuItem("GameObject/Flexible UI/Text", priority = 0)]
    public static void AddText() {
        GameObject textGO = Create<FlexibleUIText>("Text");
        TextMeshProUGUI text = textGO.GetComponent<TextMeshProUGUI>();
        text.text = "Text";
        text.alignment = TextAlignmentOptions.Center;
        PopRefCheck();
        text.font = popRef.themeSwap.allFlexibleUIThemes[popRef.themeSwap.activeIndex].fontAsset;
    }

    private static GameObject Create<T>(string objectName, GameObject go = null) where T : Object {
        GameObject instance = new GameObject(objectName, typeof(T));
        if (go == null) {
            GameObject clickedObject = Selection.activeGameObject;
            if (clickedObject != null) {
                instance.transform.SetParent(clickedObject.transform, false);
            }
        }
        else instance.transform.SetParent(go.transform, false);
        return instance;
    }

    private static void PopRefCheck() {
        if (popRef == null) popRef = FindObjectOfType<PopRef>();
    }
}