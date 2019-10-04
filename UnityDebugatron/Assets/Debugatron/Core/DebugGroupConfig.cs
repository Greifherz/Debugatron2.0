using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using DebugatronCore;

public class DebugGroupConfig : ScriptableObject
{	
	public bool HasGroup(string Name)
	{
		if(Name.Equals("")) 
			return true;
		for(int i = 0; i < DebugGroups.Count; i++)
		{
			if(DebugGroups[i].Name.Equals(Name) || Name.Equals("Default"))
				return true;
		}
		return false;
	}
	
	public void AddGroup(DebugGroup Group)
	{
		if(!HasGroup(Group.Name))
		{
			if(Group.Name.Equals("Default"))
				Group.Id = 0;
			else
				Group.Id = DebugGroups.Count;
			
			DebugGroups.Add(Group);
			Persist();
		}
	}

	public void RemoveGroup(string Name)
	{
		if(Name.Equals("Default"))
		{
			throw new Exception("[Debugatron Warning] Cannot remove Default Group.");
		}
		
		int inx = 0;
		for(int i = 0; i < DebugGroups.Count; i++)
		{
			if(DebugGroups[i].Name.Equals(Name))
			{
				inx = i;
				break;
			}
		}	
		DebugGroups.RemoveAt(inx);
		for(int i = inx; i < DebugGroups.Count; i++)
		{
			DebugGroups[i].Id--;	
		}
		Persist();
	}
	
	public List<DebugGroup> DebugGroups;
	
	
	private void Persist()
	{
		#if UNITY_EDITOR
			EditorUtility.SetDirty(this);
		#endif
	}
}


