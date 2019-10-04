using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DebugatronCore;

public class DebugatronSettingsConfig : ScriptableObject 
{
	public string DefaultGroupName = "Default";
	public Color DefaultGroupTitleColor;
	public Color DefaultGroupTextColor;
	
	//Main Flag. Public and static for user to turn on and off
	public bool DebugAllowed = true;

	//Auto init flag. Will init debugatron automatically if validate is called and debugatron is not init.
	public bool AutoInit = true;
	
	//Enables or disables timestamps on all Debug
	public bool EnableTimestamp = true;

	//Forwards Debugatron log strings for an external handler, permitting logging on a server or ingame-console
	public bool ExternalizeStrings = true;
	
	private bool timestampBeginning = true;
	private bool timestampEnd = false;
	
	//Sets the timestamp at the beginning of the debug
	public bool TimestampBeginning
	{
		get { return timestampBeginning; }
		set 
		{
			if(value)
			{
				timestampBeginning = value;
				timestampEnd = false;
			}
			else
			{
				timestampBeginning = value;
				timestampEnd = true;
			}
		}
	}
	//Sets the timestamp at the end of the debug
	public bool TimestampEnd
	{
		get { return timestampEnd; }
		set 
		{
			if(value)
			{
				timestampEnd = value;
				timestampBeginning = false;
			}
			else
			{
				timestampEnd = value;
				timestampBeginning = true;
			}
		}	
	}
	//Color of the timestamp
	public Color TimestampColor;
		
	//Enables or disables the creation of new groups during runtime
	public bool AllowNewGroups = true;
	
	//If logging to a group that doesn't exist, create it with default color
	public bool AutoCreate = false;	
	
	//If true, groups created in runtime will be stored in the settings
	public bool NewGroupPersistence = true;	
	
	//When checked logs will be stored despite AllowDebugs
	public bool StoreDebug = true;
	
	public bool AutoDump = false;    	
}
