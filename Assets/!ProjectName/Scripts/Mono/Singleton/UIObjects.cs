using System.Collections;
using TMPro;
using UnityEngine;

public class UIObjects : MonoBehaviour {
    public static UIObjects Instance { get; private set; }

    private void Awake() {
        //Singleton code
        if (Instance != null && Instance != this) Debug.LogError("Trying to instantiate a second singleton", gameObject);
        else Instance = this;
    }

    public TextMeshProUGUI debugText;

    private IEnumerator Start() {
        Debug.Log("<color=blue>Demo rotates themes in one slot. To use multiple themes at once add them to Theme Swap list</color>");
        debugText.text = "Playing demo";
        if (PopRef.Instance.allThemes.Count == 0) yield break;
        while (true) {
            for (int i = 0; i < PopRef.Instance.allThemes.Count; i++) {
                PopRef.Instance.themeSwap.allFlexibleUIThemes[0] = PopRef.Instance.allThemes[i];
                PopRef.Instance.themeSwap.Swap();
                yield return new WaitForSeconds(1f);
            }
        }
    }
}