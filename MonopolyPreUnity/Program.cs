using Autofac;
using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Managers;
using MonopolyPreUnity.Modules;
using System;

namespace MonopolyPreUnity
{
    class Program
    {
        private static GameData CreateGameData()
        {
            return new GameData();
        }

        private static IContainer CreateDiContainer()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterInstance(CreateGameData());

            containerBuilder.RegisterModule<ManagersModule>();
            containerBuilder.RegisterModule<BehaviorsModule>();

            return containerBuilder.Build();
        }

        static void Main(string[] args)
        {
            IContainer container;
            try
            {
                container = CreateDiContainer();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Startup error", e);

                Environment.Exit(-1);
                return;
            }

            var gameManager = container.Resolve<GameManager>();
            gameManager.StartGame();
        }
    }
}
