using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

using MonopolyPreUnity.Classes;

namespace MonopolyPreUnity.Managers
{
    class Serializer
    {
        private readonly GameData _gameData;

        private readonly string savesFileName = "saves.txt";
        private readonly string savesFilePath = @"saves\";

        private string GetFileName()
        {
            return DateTime.Now.ToString();
        }

        private void addSaveToSaves(string fileName)
        {
            File.AppendAllText(savesFilePath + savesFileName, fileName + ",\n");
        }

        private void deleteSaveFromSaves()
        {
            string rawData = File.ReadAllText(savesFilePath + savesFileName);
        }
        public void SaveGame()
        {
            
        }
        
        public void LoadGame()
        {

        }
    }
}
