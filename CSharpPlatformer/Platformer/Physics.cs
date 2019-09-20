using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using static Raylib.Raylib;

namespace Platformer
{
    public class PhysicsAttributes
    {
        public float drag;
        public float gravity;
        public Vector2 velocity;
        public Vector2 maxVelocity;

        public void Update(bool enableDrag)
        {
            if (enableDrag) velocity.x *= drag;
            velocity.y += gravity;
        }
    }
}
