using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorUtils : Editor
{
    private bool Change = false;

    private bool BoolField(ref bool Property, string LabelText)
    {
        bool Temp = Property;

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(LabelText, GUILayout.Width(250));
        Property = EditorGUILayout.Toggle("", Property);
        EditorGUILayout.EndHorizontal();

        if (Temp != Property)
            return true;
        return false;
    }

    private bool ColorField(ref Color Property, string LabelText)
    {
        Color TempColor = Property;

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(LabelText, GUILayout.Width(130));
        Property = EditorGUILayout.ColorField(Property);
        EditorGUILayout.EndHorizontal();

        if (!TempColor.Equals(Property))
            return true;
        return false;
    }

    private bool TextField(ref string Property, string LabelText)
    {
        string TempString = Property;

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(LabelText, GUILayout.Width(130));
        Property = EditorGUILayout.TextField(Property);
        EditorGUILayout.EndHorizontal();

        if (!TempString.Equals(Property))
            return true;
        return false;
    }
}
