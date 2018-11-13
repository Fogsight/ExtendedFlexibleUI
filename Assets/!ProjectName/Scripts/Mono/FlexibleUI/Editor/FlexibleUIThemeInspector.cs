using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FlexibleUITheme)), CanEditMultipleObjects]
public class FlexibleUIThemeInspector : Editor {

    public override void OnInspectorGUI() {
        serializedObject.Update();

        SerializedProperty prop = serializedObject.GetIterator();
        if (prop.NextVisible(true)) {
            do {
                if (prop.name == "m_Script") {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.PropertyField(prop, true, new GUILayoutOption[0]);
                    EditorGUI.EndDisabledGroup();
                }
                else if (prop.name == "Items") {
                    EditorList.Show(prop, true);
                }
                else {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(prop.name), true);
                }
            }
            while (prop.NextVisible(false));
        }

        serializedObject.ApplyModifiedProperties();
    }
}