using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    LogMethods -> string Extension
               -> Core logging

    LogSettings -> Auto-Tag
                   Groups
                   log init start / end
                   Timestamp
                   Color
                   Output

     */

namespace Debugatron2
{
    public class Core
    {
        ILogger DebugatronLogger;
        bool IsInit;

        public void Init()
        {
            if (!IsInit)
            {
                DebugatronLogger = Debug.unityLogger;
                //Get Settings
                //Log init start if checked
                //Decorate unity logger with options
                //Log init end if checked
                IsInit = true;
            }
        }


        //Simple Logging
        public void Log(string logString)
        {
            DebugatronLogger.Log(logString);
        }



        public void LogWarning(string tag, string logString)
        {
            DebugatronLogger.LogWarning(tag, logString);
        }

        public void LogWarning(string tag, string logString, Object context)
        {
            DebugatronLogger.LogWarning(tag, logString, context);
        }



        public void LogError(string tag, string logString)
        {
            DebugatronLogger.LogError(tag, logString);
        }

        public void LogError(string tag, string logString, Object context)
        {
            DebugatronLogger.LogError(tag, logString, context);
        }
    }

}

