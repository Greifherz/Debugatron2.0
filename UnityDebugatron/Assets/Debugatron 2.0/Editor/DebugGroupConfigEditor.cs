using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using DebugatronCore;

[CustomEditor(typeof(DebugGroupConfig))]
public class DebugGroupConfigEditor : Editor
{
    bool hasChange;

    Vector2 scroll;
    DebugGroup groupToAdd;

    public override void OnInspectorGUI()
    {
        if (groupToAdd == null)
            groupToAdd = new DebugGroup("Group Name", Color.white, Color.white);

        DebugGroupConfig Config = (DebugGroupConfig)target;

        Config.hideFlags = HideFlags.None;

        #region Add Group
        GUILayout.Label("Add Group", EditorStyles.boldLabel);

        GUILayout.BeginVertical("Box");

        TextField(ref groupToAdd.Name, "Group Name");

        if (!groupToAdd.Name.Equals("") && Config.HasGroup(groupToAdd.Name))
        {
            var l = groupToAdd.Name.Equals("Default") ? "Group name reserved. Check settings." : "Group name already being used.";
            GUILayout.Label(l, EditorStyles.boldLabel);
        }
        else if (groupToAdd.Name.Equals(""))
        {
            GUILayout.Label("Invalid Group Name.", EditorStyles.boldLabel);
        }

        ColorField(ref groupToAdd.GroupColor, "Group Color");
        ColorField(ref groupToAdd.DebugColor, "Debug Color");

        EditorGUILayout.Space();

        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("Add Group") && !Config.HasGroup(groupToAdd.Name))
        {
            Config.AddGroup(groupToAdd);
            groupToAdd = new DebugGroup("Group Name", Color.white, Color.white);
        }
        GUI.backgroundColor = Color.white;

        GUILayout.EndVertical();
        GUIHorizontalLine();
        #endregion

        #region Registered Groups
        GUILayout.Label("Registered Debug Groups", EditorStyles.boldLabel);

        scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.ExpandHeight(true));

        if (Config.DebugGroups == null)
        {
            Config.DebugGroups = new List<DebugGroup>();
        }

        if (Config.DebugGroups.Count > 0)
        {
            for (int i = 0; i < Config.DebugGroups.Count; i++)
            {
                DebugGroup Group = Config.DebugGroups[i];
                EditorGUILayout.LabelField("Id: " + Group.Id.ToString(), EditorStyles.boldLabel);

                GUILayout.BeginVertical("Box");
                EditorGUIUtility.labelWidth = 100;

                if (i == 0)
                {
                    GUILayout.Label("Default");
                }
                else
                {
                    TextField(ref Group.Name, "Group Name");
                }

                if (i > 0)
                {
                    BoolField(ref Group.Allowed, "Enabled?");
                }

                BoolField(ref Group.Store, "Can Store?");
                BoolField(ref Group.Externalize, "Can Externalize?");
                ColorField(ref Group.GroupColor, "Group Color");
                ColorField(ref Group.DebugColor, "Debug Color");

                GUILayout.Space(5);
                GUILayout.BeginHorizontal();

                if (Application.isPlaying)
                {
                    if (GUILayout.Button("Dump Group"))
                    {
                        Debugatron.DumpLogHistory(Group);
                    }
                }

                GUI.backgroundColor = Color.red;
                if (i > 0)
                {
                    if (GUILayout.Button("Remove Group"))
                    {
                        Config.RemoveGroup(Group.Name);
                        i--;
                    }
                }
                GUI.backgroundColor = Color.white;
                GUILayout.EndHorizontal();

                GUILayout.EndVertical();
                EditorGUILayout.Space();
            }
        }
        else
        {
            DebugGroup DefaultGroup = new DebugGroup("Default", Color.white, Color.white);
            Config.AddGroup(DefaultGroup);
        }

        EditorGUILayout.EndScrollView();
        #endregion

        if (hasChange)
        {
            hasChange = false;
            EditorUtility.SetDirty(target);
        }
    }

    void BoolField(ref bool property, string labelText)
    {
        bool b = property;

        EditorGUILayout.BeginHorizontal();
        property = EditorGUILayout.Toggle(labelText, property);
        EditorGUILayout.EndHorizontal();

        if (b != property)
            hasChange = true;
    }

    void ColorField(ref Color property, string labelText)
    {
        Color c = property;

        EditorGUILayout.BeginHorizontal();
        property = EditorGUILayout.ColorField(labelText, property);
        EditorGUILayout.EndHorizontal();

        if (!c.Equals(property))
            hasChange = true;
    }

    void TextField(ref string property, string labelText)
    {
        string s = property;

        EditorGUILayout.BeginHorizontal();
        property = EditorGUILayout.TextField(labelText, property);
        EditorGUILayout.EndHorizontal();

        if (!s.Equals(property))
            hasChange = true;
    }

    void GUIHorizontalLine()
    {
        EditorGUILayout.Space();
        var rect = EditorGUILayout.BeginHorizontal();
        Handles.color = Color.gray;
        Handles.DrawLine(new Vector2(rect.x - 15, rect.y), new Vector2(rect.width + 15, rect.y));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
    }
}