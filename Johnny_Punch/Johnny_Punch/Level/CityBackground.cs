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
    class CityBackground :Environment
    {
        public CityBackground(Texture2D tex, Vector2 pos )
            :base(pos, tex)
        {

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }
    }
}
