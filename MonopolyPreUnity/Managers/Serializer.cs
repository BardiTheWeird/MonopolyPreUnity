using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary;

using MonopolyPreUnity.Classes;
using System.Runtime.InteropServices.WindowsRuntime;

namespace MonopolyPreUnity.Managers
{
    class Serializer
    {
        private GameData _gameData;

        private readonly string savesFilePath = @"saves\";

        public List<string> getAllSaveNames()
        {
            List<string> name = new List<string>();
            FileInfo[] Files;
            try
            {
                DirectoryInfo d = new DirectoryInfo(Directory.GetCurrentDirectory() + savesFilePath);
                Files = d.GetFiles("*.dat");
            }
            catch (DirectoryNotFoundException)
            {
                return name;
            }

            foreach (FileInfo file in Files)
            {
                name.Add(file.Name.Remove(file.Name.Length - 4));
            }

            return name;
        }

        public void saveGame(string saveName)
        {
            BinaryFormatter serializerBinary = new BinaryFormatter();

            if (!Directory.Exists(Directory.GetCurrentDirectory() + savesFilePath))
                Directory.CreateDirectory(savesFilePath);

            using (FileStream f = new FileStream(savesFilePath + saveName + ".dat", FileMode.OpenOrCreate))
            {
                serializerBinary.Serialize(f, _gameData);
                Logger.Log($"Game progress was successfully saved to file {saveName + ".dat"}.");
            }
        }

        public GameData LoadGameProgress(string saveName)
        {
            GameData gamedataobj;

            BinaryFormatter serializerBinary = new BinaryFormatter();

            using (FileStream f = new FileStream(savesFilePath + saveName + ".dat", FileMode.Open))
            {
                gamedataobj = (GameData)serializerBinary.Deserialize(f);
            }
            return gamedataobj;
        }

        public GameData LoadStaticData()
        {
            throw new NotImplementedException();
        }

        public GameData LoadGame(string saveName)
        {
            GameData progressdata = LoadGameProgress(saveName);
            GameData staticdata = LoadStaticData();
            
            //do some merdging stuff
            //don`t forget to change _gameData dependency

            throw new NotImplementedException();
        }
    }
}
