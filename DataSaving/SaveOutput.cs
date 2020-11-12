using MonopolyPreUnity.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataSaving
{
    static class SaveOutput
    {
        static string SavedDataPath = @"..\..\..\..\DataSaving\SavedData";

        public static void SaveOutputLog(this Context context)
        {
            var log = context.OutputString;
            File.WriteAllText(SavedDataPath + @"\OutputLog\Log1.txt", log);
        }

        #region misc
        //static string GetFilePathUniqueName(string directoryPath, string fileName)
        //{
        //    var files = Directory.GetFiles(directoryPath);

        //}
        #endregion
    }
}
