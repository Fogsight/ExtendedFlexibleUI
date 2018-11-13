using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Persistant Objects (popular) References
/// </summary>
[ExecuteInEditMode]
public class PopRef : MonoBehaviour {
    public static PopRef Instance { get; private set; }

    private void Awake() {
        //Singleton code
        if (Instance != null && Instance != this) Debug.LogError("Trying to instantiate a second singleton", gameObject);
        else Instance = this;
    }

    [Header("Flexible UI")]
    [Header("Persistant Objects References")]
    public ThemeSwap themeSwap;
    [ReadOnlyTextArea] public string note = "Setting list to zero will result in lockup";
    public List<FlexibleUITheme> allThemes;

    private void OnValidate() {
        //Cleanup deleted Themes
        for (int i = allThemes.Count - 1; i >= 0; i--) {
            if (allThemes[i] == null) allThemes.RemoveAt(i);
        }
    }
}