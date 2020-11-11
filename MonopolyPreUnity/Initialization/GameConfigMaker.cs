using MonopolyPreUnity.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace MonopolyPreUnity.Initialization
{
    static class GameConfigMaker
    {
        public static GameConfig DefaultGameConfig() =>
            ReadGameConfig(@"..\..\..\..\MonopolyPreUnity\Resources\defaultGameConfig.xml");

        #region read/write
        public static GameConfig ReadGameConfig(string filePath)
        {
            var serializer = new XmlSerializer(typeof(GameConfig));
            var textReader = new StreamReader(filePath);

            var config = (GameConfig)serializer.Deserialize(textReader);

            return config;
        }

        public static void WriteGameConfig(GameConfig config, string filePath)
        {
            var serializer = new XmlSerializer(typeof(GameConfig));
            var textWriter = new StreamWriter(filePath);

            serializer.Serialize(textWriter, config);
        }
        #endregion
    }
}
