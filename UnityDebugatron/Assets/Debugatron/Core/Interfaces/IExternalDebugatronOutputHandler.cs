using System;

namespace DebugatronCore
{
	public interface IExternalDebugatronOutputHandler
	{
		void HandleDebugatronString(string DebugatronString);
	}
}

