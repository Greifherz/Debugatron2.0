/***************************************************************
 * 					Debugatron v0.9
 * 
 * Author: Douglas Barbará 					OCT-2014
 * 
 * Debug Group Class
 * 		Organize your debugs in groups
 ***************************************************************************************/


using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DebugatronCore
{
	/// <summary>
	/// Debug group class.
	/// Used for grouping DebugObjs and configurations of their display.
	/// </summary>
	[Serializable]
	public class DebugGroup
	{
		//Debug group name
		public string Name;
		
		//Group Id
		public int Id;
		
		//If allowed to debug
		public bool Allowed;

		//If allowed to send to externals
		public bool Externalize;
		
		//If will store debugs
		public bool Store;
		
		//Group color in UnityColor
		public Color GroupColor;
		
		//Debug text color in UnityColor
		public Color DebugColor;
		
		//List of debugs associated with this group
		public List<DebugObj> DebugObjs;
		
		/// <summary>
		/// Initializes a new instance of the <see cref="DebugatronCore.DebugGroup"/> class.
		/// </summary>
		/// <param name='_Name'>
		/// Name
		/// </param>
		/// <param name='_GColor'>
		/// Group Color in string
		/// </param>
		/// <param name='_DColor'>
		/// Debug Text Color in string
		/// </param>
		public DebugGroup (string _Name,string _GColor,string _DColor)
		{
			Name = _Name;
			GroupColor = HexToColor(_GColor);
			DebugColor = HexToColor(_DColor);
			Allowed = true;
			Externalize = false;
			DebugObjs = new List<DebugObj>();
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="DebugatronCore.DebugGroup"/> class.
		/// </summary>
		/// <param name='_Name'>
		/// Name
		/// </param>
		/// <param name='_GColor'>
		/// Group Color in UnityColor
		/// </param>
		/// <param name='_DColor'>
		/// Debug Text Color in string
		/// </param>
		public DebugGroup (string _Name,Color _GColor,string _DColor)
		{
			Name = _Name;
			GroupColor = _GColor;
			DebugColor = HexToColor(_DColor);
			Allowed = true;
			Externalize = false;
			DebugObjs = new List<DebugObj>();
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="DebugatronCore.DebugGroup"/> class.
		/// </summary>
		/// <param name='_Name'>
		/// Name
		/// </param>
		/// <param name='_GColor'>
		/// Group Color in string
		/// </param>
		/// <param name='_DColor'>
		/// Debug Text Color in UnityColor
		/// </param>
		public DebugGroup (string _Name,string _GColor,Color _DColor)
		{
			Name = _Name;
			GroupColor = HexToColor(_GColor);
			DebugColor = _DColor;
			Allowed = true;
			Externalize = false;
			DebugObjs = new List<DebugObj>();	
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="DebugatronCore.DebugGroup"/> class.
		/// </summary>
		/// <param name='_Name'>
		/// Name
		/// </param>
		/// <param name='_GColor'>
		/// Group Color in UnityColor
		/// </param>
		/// <param name='_DColor'>
		/// Debug Text Color in UnityColor
		/// </param>
		public DebugGroup (string _Name,Color _GColor,Color _DColor)
		{
			Name = _Name;
			GroupColor = _GColor;
			DebugColor = _DColor;
			Allowed = true;
			Externalize = false;
			DebugObjs = new List<DebugObj>();
		}
		
		public void Write(string Message)
		{
			if(DebugObjs == null)
				DebugObjs = new List<DebugObj>();
		
			DebugObj DebugObject = new DebugObj(this,Message);
			
			if(Store)
				DebugObjs.Add(DebugObject);
			
			if(Allowed)
				DebugObject.Write();
		}
		
		public void DumpGroup(System.IO.StreamWriter FileWriter)
		{
			if(Store)
			{
				FileWriter.WriteLine("*********************************************************");
				FileWriter.WriteLine("******* " + Name + " GROUP DUMP START ************");
				FileWriter.WriteLine("*********************************************************");
				
				if(DebugObjs != null && DebugObjs.Count > 0)
				{
					DebugObjs.ForEach(x => x.DumpObject(FileWriter));
				}
				else
				{
					FileWriter.WriteLine(
						Environment.NewLine +
						"         ********** EMPTY GROUP ***************" +
						Environment.NewLine
						);	
				}
				
				FileWriter.WriteLine(Environment.NewLine + Environment.NewLine + Environment.NewLine);
			}
		}
		
		/// <summary>
		/// Convert UnityColor to HEX color, used for rich text colouring.
		/// </summary>
		/// <returns>
		/// The HEX string.
		/// </returns>
		/// <param name='color'>
		/// UnityColor to be converted.
		/// </param>
		public static string ColorToHex(Color32 color)
		{
			 return "#" + color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");	
		}
		
		/// <summary>
		/// Convert HEX to UnityColor color, used for rich text colouring.
		/// </summary>
		/// <returns>
		/// UnityColor
		/// </returns>
		/// <param name='color'>
		/// Hex to be converted.
		/// </param>
		public static Color32 HexToColor(string Hex)
		{
			if(Hex.Length < 8)
			{
				switch(Hex)
				{
				case "red" :
					return Color.red;
				case "yellow" :
					return Color.yellow;
				case "blue" :
					return Color.blue;
				case "black" :
					return Color.black;
				case "grey" :
					return Color.grey;
				case "gray" :
					return Color.gray;
				case "cyan":
					return Color.cyan;
				case "green":
					return Color.green;
				case "magenta":
					return Color.magenta;
				case "white":
					return Color.white;
				default :
					return Color.white;
				}
			}
			else
			{
				byte r = byte.Parse(Hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber);
				byte g = byte.Parse(Hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
				byte b = byte.Parse(Hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
				return new Color32(r,g,b, 255);
			}
		}
	}
}

