using MonopolyPreUnity.Components;
using MonopolyPreUnity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonopolyPreUnity.Systems
{
    class BulletTankCollisionSystem : ISystem
    {
        private readonly Context _context;

        public void Execute()
        {
            var collisions = _context.GetComponents<CollisionEvent>();
            var filtered = collisions
                .Where(c => c.CollidedA.ContainsComponent<Bullet>() && c.CollidedB.ContainsComponent<Tank>()
                || c.CollidedA.ContainsComponent<Tank>() && c.CollidedB.ContainsComponent<Bullet>());

            foreach (var collision in collisions)
            {
                // do stuff
            }
            collisions.ForEach(c => _context.Remove(c));
        }

        public BulletTankCollisionSystem(Context context) =>
            _context = context;
    }
}
