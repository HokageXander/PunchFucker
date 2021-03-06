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
    class Watermelon : Item
    {

        public Watermelon(Texture2D tex, Vector2 pos) : base(tex, pos)
        {

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.playerShadow, new Vector2(pos.X + width / 2, pos.Y + height), null, new Color(0, 0, 0, 120), 0f, new Vector2(width / 2, height - height / 1.3f), 0.65f, SpriteEffects.None, 0.1f);
            base.Draw(spriteBatch);
        }
    }
}
