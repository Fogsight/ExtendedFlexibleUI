using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/UI/ThemeSwap")]
public class ThemeSwap : ScriptableObject {
    private List<FlexibleUITheme> previousFlexibleUITheme;
    private PopRef popRef;

    [ReadOnlyTextArea(2)]
    public string note = "Adding more than one of the same Theme is disallowed because it will merge their contents!";

    [Tooltip("Determines which theme is used for new items")]
    public int activeIndex = 0;
    public List<FlexibleUITheme> allFlexibleUIThemes;

    private void OnEnable() {
        if (popRef == null) popRef = FindObjectOfType<PopRef>();

        previousFlexibleUITheme = allFlexibleUIThemes.ToList();
    }

    private void OnValidate() {
        Swap();
    }

    public void Swap() {
        int activeThemesCount = allFlexibleUIThemes.Count;
        if (previousFlexibleUITheme == null) OnEnable();

        //Check if activeIndex is within the existing range
        if (activeIndex > activeThemesCount - 1) activeIndex = 0;

        //Adds unique themes to the new elements
        if (activeThemesCount > previousFlexibleUITheme.Count) {
            //if there are no spare themes
            if (activeThemesCount > popRef.allThemes.Count) {
                allFlexibleUIThemes = previousFlexibleUITheme.ToList();
                Debug.LogError("Themes cannot be used twice due to unique collections, create new Theme first", popRef.allThemes[0]);
            }
            else {
                int indexLimit = activeThemesCount;
                allFlexibleUIThemes = previousFlexibleUITheme.ToList();
                foreach (var item in popRef.allThemes) {
                    if (!allFlexibleUIThemes.Contains(item)) {
                        allFlexibleUIThemes.Add(item);
                        if (allFlexibleUIThemes.Count == indexLimit) break;
                    }
                }
                OnEnable();
            }
        }
        else if (allFlexibleUIThemes.Count < previousFlexibleUITheme.Count) OnEnable();
        //Check for repeating Theme usage, reset if not unique
        else {
            bool allUnique = allFlexibleUIThemes.GroupBy(x => x).All(g => g.Count() == 1);
            if (!allUnique) {
                allFlexibleUIThemes = previousFlexibleUITheme.ToList();
                Debug.LogError("Not allowed to use the same Theme twice. This will merge collections", this);
            }
        }

        activeThemesCount = allFlexibleUIThemes.Count;
        for (int i = 0; i < activeThemesCount; i++) {
            if (allFlexibleUIThemes[i] != previousFlexibleUITheme[i]) {
                for (int j = 0; j < previousFlexibleUITheme[i].Items.Count; j++) {
                    FlexibleUI flexibleUI = previousFlexibleUITheme[i].Items[j] as FlexibleUI;
                    flexibleUI.flexibleUITheme = allFlexibleUIThemes[i];
                    flexibleUI.flexibleUITheme.Add(flexibleUI);
                    flexibleUI.flexibleUITheme = allFlexibleUIThemes[i];
                    flexibleUI.OnSkinUI();
                }
                previousFlexibleUITheme[i].Items.Clear();
            }
        }

        for (int i = 0; i < activeThemesCount; i++) {
            FlexibleUITheme flexibleUITheme = allFlexibleUIThemes[i];
            int subCount = flexibleUITheme.Items.Count - 1;
            for (int j = subCount; j >= 0; j--) {
                //take out leftover empty items
                if (flexibleUITheme.Items[j] == null) {
                    flexibleUITheme.Remove(flexibleUITheme.Items[j]);
                    continue;
                }
                ((flexibleUITheme.Items[j]) as FlexibleUI).flexibleUITheme = allFlexibleUIThemes[i];
            }
        }

        for (int i = 0; i < activeThemesCount; i++) {
            allFlexibleUIThemes[i].OnValidate();
            previousFlexibleUITheme[i] = allFlexibleUIThemes[i];
        }
    }
}