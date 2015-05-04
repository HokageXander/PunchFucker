using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Johnny_Punch
{
    class StandardEnemy : Enemy
    {
        
        public StandardEnemy(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            width /= 1;
            aggroRadius = 250;
            animationBox = new Rectangle(0, 0, width, height);
            damageToPlayer -= 1;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
