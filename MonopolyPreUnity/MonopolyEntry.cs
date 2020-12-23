using Autofac;
using MonopolyPreUnity.Actions;
using MonopolyPreUnity.Behaviors;
using MonopolyPreUnity.Behaviors.PlayerLanded;
using MonopolyPreUnity.Behaviors.Rent;
using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Initialization;
using MonopolyPreUnity.Modules;
using MonopolyPreUnity.RequestHandlers;
using MonopolyPreUnity.RequestHandlers.HSScenario;
using MonopolyPreUnity.Systems;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MonopolyPreUnity
{
    class MonopolyEntry
    {
        static void Main(string[] args)
        {
            GameConfig gameConfig = GameConfigMaker.DefaultGameConfig();
            MockContext.ParseDefaultMap(gameConfig, null);
        }

        public static async Task RunSystemsContinuousAsync(SystemsBag systemsBag, RenderCommunications renderCommunications)
        {

            while (true)
            {
                try
                {
                    systemsBag.Execute();
                    await Task.Delay(1);
                }
                catch (GameOverException e)
                {
                    break;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
        }
    }
}
