using System;
using UnityEngine;

namespace DebugatronCore
{
	public class AutoDumper : MonoBehaviour
	{
		void Awake()
		{
			DontDestroyOnLoad(gameObject);
		}

		void OnApplicationQuit() 
		{
			Debugatron.DumpLogHistory();
		}
	}
}

