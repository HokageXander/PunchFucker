﻿using System;
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
        public int directionNr;

        public Bullet(Texture2D tex, Vector2 pos, int directionNr) : base(tex, pos)
        {
            this.directionNr = directionNr;
            floatLayerOffsetY = 40;
        }

        public override void Update(GameTime gameTime)
        {
            bulletTimer += gameTime.ElapsedGameTime.TotalSeconds;

            direction = new Vector2(directionNr, 0);
            direction.Normalize();

            velocity = 6 * direction;
            pos += velocity;

            if (direction.X < 0)
                spriteEffects = SpriteEffects.None;
            else
                spriteEffects = SpriteEffects.FlipHorizontally;
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.playerShadow, new Vector2(pos.X + width/2, pos.Y + 75), null, new Color(0, 0, 0, 120), 0f, new Vector2(63 / 2, 21 / 2), 0.5f, SpriteEffects.None, 0.1f);

            base.Draw(spriteBatch);
        }
    }
}
