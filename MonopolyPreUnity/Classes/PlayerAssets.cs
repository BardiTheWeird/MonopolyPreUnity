using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonopolyPreUnity.Classes
{
    class PlayerAssets
    {
        public int PlayerId { get; set; }
        public List<int> Properties { get; set; }
        public int Cash { get; set; }
        public int JailCards { get; set; }

        public PlayerAssets(int playerId, List<int> properties, int cash, int jailCards)
        {
            PlayerId = playerId;
            Properties = properties;
            Cash = cash;
            JailCards = jailCards;
        }

        public PlayerAssets(Player player)
        {
            PlayerId = player.Id;
            Properties = player.Properties.ToList();
            Cash = player.Cash;
            JailCards = player.JailCards;
        }

        public PlayerAssets()
        {
        }
    }
}
