using UnityEngine;
using System.Collections;
using UnityEditor;

/// <summary>
/// Debugatron settings.
/// </summary>
public class DebugatronSettings 
{
	//The function called when you click on the settings context menu under the Debugatron tab.
	//Loads the setting, if none exists, create a new one. Selects the Settings asset at the end.
	[MenuItem("Debugatron/Settings")]
	private static void ShowSettings()
	{
		Selection.activeObject = DebugatronCore.Debugatron.LoadSettings();
	}	
	
	[MenuItem("Debugatron/Debug Groups")]
	private static void ShowDebugGroups()
	{
		Selection.activeObject = DebugatronCore.Debugatron.LoadDebugGroups();
	}	
	
}
