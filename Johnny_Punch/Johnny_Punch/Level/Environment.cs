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
     public class Environment
    {
         private Vector2 pos;
         private Texture2D tex;

        public Environment(Vector2 pos, Texture2D tex)
         {
             this.pos = pos;
             this.tex = tex;
         }

         public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }
    }
}
