using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Johnny_Punch
{

    class Bullet : BossAttacks
    {
        public Vector2 velocity, direction;
        public double bulletTimer;
        //public bool hasHit;
        public int directionNr;

        public Bullet(Texture2D tex, Vector2 pos, int directionNr) : base(tex, pos)
        {
            this.directionNr = directionNr;
        }

        public override void Update(GameTime gameTime)
        {
            bulletTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

            direction = new Vector2(directionNr, 0);
            direction.Normalize();

            velocity = 6 * direction;
            pos += velocity;
            base.Update(gameTime);
        }

    }
}
