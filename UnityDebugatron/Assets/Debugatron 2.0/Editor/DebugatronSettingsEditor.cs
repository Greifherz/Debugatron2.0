using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(DebugatronSettingsConfig))]
public class DebugatronSettingsEditor : Editor
{	
	private bool Change = false;
	
	public override void OnInspectorGUI ()
	{
		bool Temp = false;
		string TempString = "";
		Color TempColor = new Color();
		
		DebugatronSettingsConfig Config = (DebugatronSettingsConfig)target;
		
		//GUILayout.Label("    Default Group Settings",EditorStyles.boldLabel);
		
		//TempString = Config.DefaultGroupName;
		
		//EditorGUILayout.BeginHorizontal();
		//GUILayout.Label("Default Group Name",GUILayout.Width(130));
		//TempString = Config.DefaultGroupName;
		//Config.DefaultGroupName = EditorGUILayout.TextField(Config.DefaultGroupName);
		//if(!Config.DefaultGroupName.Equals(TempString))
		//	Change = true;
		//EditorGUILayout.EndHorizontal();
		
		//if(!TempString.Equals(Config.DefaultGroupName))
		//	Change = true;
		
		//ColorField(ref Config.DefaultGroupTitleColor,"Default Group Color");
		
		//ColorField(ref Config.DefaultGroupTextColor,"Default Debug Color");
		
		//EditorGUILayout.Separator();
		GUILayout.Label("    Debugatron Settings",EditorStyles.boldLabel);
		
		BoolField(ref Config.DebugAllowed,"Allow Debugging");

		BoolField(ref Config.AutoInit,"Allow Auto initialization");
		
		BoolField(ref Config.StoreDebug,"Debug Storage");
		
		BoolField(ref Config.LogErrorExceptionAllowed,"Allow throwing exception");
				
		BoolField(ref Config.EnableTimestamp,"Enable Timestamp");
		
		if(Config.EnableTimestamp)
		{
			ColorField(ref Config.TimestampColor,"    Timestamp Color");
			
			Temp = Config.TimestampBeginning;
			
			EditorGUILayout.BeginHorizontal();	
			GUILayout.Label("    Position :",GUILayout.Width(75));		
			GUILayout.Label("Beginning",GUILayout.Width(65));
			Config.TimestampBeginning = EditorGUILayout.Toggle("",Config.TimestampBeginning,GUILayout.Width(20));
			
			GUILayout.Label("",GUILayout.Width(15));
			GUILayout.Label("End",GUILayout.Width(25));
			Config.TimestampEnd = EditorGUILayout.Toggle("",Config.TimestampEnd,GUILayout.Width(20));
			EditorGUILayout.EndHorizontal();			
			
			if(Temp != Config.TimestampBeginning)
				Change = true;
			
			EditorGUILayout.Separator();
		}
		
		BoolField(ref Config.AllowNewGroups,"Allow New Group Creation");
		
		BoolField(ref Config.AutoCreate,"Allow Auto Creation of Groups");
		
		BoolField(ref Config.NewGroupPersistence,"New Group Persistence");
		
		
		EditorGUILayout.Separator();
		GUILayout.Label("    Dump Settings",EditorStyles.boldLabel);
		
		if(Application.isPlaying)
		{
			EditorGUILayout.Separator();
			if(GUILayout.Button("Full Dump",GUILayout.Width(250)))
			{
				DebugatronCore.Debugatron.DumpLogHistory();
			}
			EditorGUILayout.Separator();
		}
		
		BoolField(ref Config.AutoDump,"Automatically Dump on exit");
		
		BoolField(ref Config.OutputToDesktop,"Output to Desktop");
		
		BoolField(ref Config.OutputToProject,"Output to Project");
		
		BoolField(ref Config.OutputToPersistentPath,"Output to Persistent Path");
		
		BoolField(ref Config.CustomOutputPath,"Custom Output Path");
		
		if(Config.CustomOutputPath)
		{
			GUILayout.Box("Dump Output Path", GUILayout.Height(100),GUILayout.Width(275));
			
			GUILayout.BeginArea(new Rect(25,450,250,100));
			EditorGUILayout.BeginVertical();
			TempString = Config.DumpOutputPath;
			Config.DumpOutputPath = EditorGUILayout.TextArea(Config.DumpOutputPath,GUILayout.Height(55));
			if(!Config.DumpOutputPath.Equals(TempString))
				Change = true;
			EditorGUILayout.EndVertical();
			GUILayout.EndArea();
		}
		
		if(Change)
		{	
			Change = false;
			EditorUtility.SetDirty(target);
		}
	}

    private void BoolField(ref bool Property, string LabelText)
    {
        bool Temp = Property;

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(LabelText, GUILayout.Width(250));
        Property = EditorGUILayout.Toggle("", Property);
        EditorGUILayout.EndHorizontal();

        if (Temp != Property)
            Change = true;
    }

    private void ColorField(ref Color Property, string LabelText)
    {
        Color TempColor = Property;

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(LabelText, GUILayout.Width(130));
        Property = EditorGUILayout.ColorField(Property);
        EditorGUILayout.EndHorizontal();

        if (!TempColor.Equals(Property))
            Change = true;
    }

    private void TextField(ref string Property, string LabelText)
    {
        string TempString = Property;

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(LabelText, GUILayout.Width(130));
        Property = EditorGUILayout.TextField(Property);
        EditorGUILayout.EndHorizontal();

        if (!TempString.Equals(Property))
            Change = true;
    }

}
