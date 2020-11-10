using Autofac;
using MonopolyPreUnity.Actions;
using MonopolyPreUnity.Behaviors;
using MonopolyPreUnity.Behaviors.PlayerLanded;
using MonopolyPreUnity.Behaviors.Rent;
using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Initialization;
using MonopolyPreUnity.Managers;
using MonopolyPreUnity.Modules;
using MonopolyPreUnity.RequestHandlers;
using MonopolyPreUnity.RequestHandlers.HotSeatScenario;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace MonopolyPreUnity
{
    class Program
    {
        static void Main(string[] args)
        {
            IContainer container;
            try
            {
                container = InitContainerBuilder.CreateDiContainer();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Startup error:\n" + e);

                Environment.Exit(-1);
                return;
            }

            //var gameManager = container.Resolve<GameManager>();
            //gameManager.StartGame();
        }
    }
}
