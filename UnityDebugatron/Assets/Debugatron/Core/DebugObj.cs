/***************************************************************
 * 					Debugatron v0.9
 * 
 * Author: Douglas Barbará 					OCT-2014
 * 
 * Debug Object Class
 * 		A single debug object. Incorporates it's own write method.
 * 
 ***************************************************************************************/


using System;
using UnityEngine;

namespace DebugatronCore
{
    public class DebugObj
    {
        //It's body
        public string RawText;
        public DateTime TimeCreated;
        public string TreatedText;

        //Reference for the group it belongs to
        public DebugGroup GroupIn;

        /// <summary>
        /// Initializes a new instance of the <see cref="DebugatronCore.DebugObj"/> class.
        /// </summary>
        /// <param name='Group'>
        /// DebugGroup it should belong to.
        /// </param>
        /// <param name='str'>
        /// Body of the log.
        /// </param>
        public DebugObj(DebugGroup Group, string str)
        {
            RawText = str;
            GroupIn = Group;
            TimeCreated = DateTime.UtcNow;
        }

        /// <summary>
        /// Write this instance.
        /// </summary>
        public void Write()
        {
            string OutStr = "";
            string StartGroupTag = "<color=" + DebugGroup.ColorToHex(GroupIn.GroupColor) + "> ";
            string StartDebugTag = "<color=" + DebugGroup.ColorToHex(GroupIn.DebugColor) + "> ";
            string EndTag = " </color> ";

            TreatedText = StartDebugTag + RawText.Replace("\n", EndTag + "\n" + StartDebugTag) + EndTag;

            OutStr += StartGroupTag + "[" + GroupIn.Name + "]" + EndTag + ":" + TreatedText;

            if (Debugatron.Settings.EnableTimestamp)
            {
                string StartTagTimestampColor = "<color=" + DebugGroup.ColorToHex(Debugatron.Settings.TimestampColor) + ">";

                if (Debugatron.Settings.TimestampBeginning)
                {
                    OutStr = CreateTimeStamp() + OutStr;
                }
                else
                {
                    OutStr += "\n " + StartTagTimestampColor + CreateTimeStamp() + EndTag + "\n";
                }
            }

            //Dispatch string to external handlers
            if (GroupIn.Externalize)
            {
                if (Debugatron.ExternalHandlers != null && Debugatron.ExternalHandlers.Count > 0)
                {
                    for (int i = 0; i < Debugatron.ExternalHandlers.Count; i++)
                    {
                        if (Debugatron.ExternalHandlers[i] == null)
                        {
                            Debugatron.ExternalHandlers.RemoveAt(i);
                            i--;
                            continue;
                        }
                        IExternalDebugatronOutputHandler Handler = Debugatron.ExternalHandlers[i];
                        Handler.HandleDebugatronString(OutStr);
                    }
                }
            }

            Debug.Log(OutStr);
        }

        public string CreateTimeStamp()
        {
            TimeSpan Now = TimeCreated - Debugatron.TimestampStart;

            int Seconds = Now.Seconds;
            int Minutes = Now.Minutes;
            int Millis = Now.Milliseconds;

            return "(" + Minutes.ToString() + ":" + Seconds.ToString() + ":" + Millis.ToString() + ") ";
        }

        public void DumpObject(System.IO.StreamWriter FileWriter)
        {
            string OutputString = "    " +
                "\n*******************************\n" +
                    TimeCreated.ToString() +
                "\n*******************************\n" +
                    RawText +
                "\n--END LOG--";

            OutputString = OutputString.Replace("\n", Environment.NewLine);

            FileWriter.Write(OutputString);
            FileWriter.WriteLine(Environment.NewLine);
        }
    }
}