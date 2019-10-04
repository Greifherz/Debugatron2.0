//using System;
//using System.Collections;
//using System.Collections.Generic;

//public static class DebugatronIListExtensions
//{
//    public static T FindFirst<T>(this IList<T> source, Func<T, bool> condition)
//    {
//        foreach(T item in source)
//            if(condition(item))
//                return item;
//        return default(T);
//    }

//    public static void DebugatroRegisterSelf(this DebugatronCore.IExternalDebugatronOutputHandler self)
//    {
//        DebugatronCore.Debugatron.AddExternalOutput(self);
//    }

//    public static void DebugatronDeregisterSelf(this DebugatronCore.IExternalDebugatronOutputHandler self)
//    {
//        DebugatronCore.Debugatron.RemoveExternalOutput(self);
//    }
//}