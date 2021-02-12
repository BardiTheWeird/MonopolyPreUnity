using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components
{
    class CollisionEvent : IEntityComponent
    {
        public Entity.Entity CollidedA { get; set; }
        public Entity.Entity CollidedB { get; set; }
    }

    class Bullet : IEntityComponent
    {

    }
    class Tank : IEntityComponent
    {

    }
    class Wall : IEntityComponent
    {

    }
}
