using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DataSaving
{
    public class Logger
    {
        #region static props
        static string SavedDataPath = @"..\..\..\..\DataSaving\SavedData\";
        static string OutputLogPath = SavedDataPath + @"OutputLog\";
        #endregion

        #region props
        public string CurLogFileName { get; set; }
        #endregion

        #region save/clear/append
        public void SaveOutputLog(string log) =>
            File.WriteAllText(OutputLogPath + CurLogFileName, log);

        public void ClearOutputLog() =>
            File.WriteAllText(OutputLogPath + CurLogFileName, "");

        public void AppendLine(string log) { }
        #endregion

        #region misc
        static string GetNextFilename(string directoryPath, string fileName, string fileExtension)
        {
            var biggestNum = Directory.GetFiles(directoryPath)
                .Select(file => Regex.Match(file, $@"\\{fileName}\s(\d+).{fileExtension}"))
                .Where(match => match.Success)
                .Select(match => Convert.ToInt32(match.Groups[1].Value))
                .Aggregate<int, int?>(null, (biggest, curNum) =>
                {
                    if (!biggest.HasValue)
                        return curNum;
                    return biggest < curNum ? curNum : biggest;
                });

            var nextNum = (biggestNum + 1) ?? 1;
            return $"{fileName} {nextNum}.{fileExtension}";
        }
        #endregion

        #region ctor
        /*
        public Logger() =>
            CurLogFileName = GetNextFilename(OutputLogPath, "Log", "txt");
        */
        #endregion
    }
}
