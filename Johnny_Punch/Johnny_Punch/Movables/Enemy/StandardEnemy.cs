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
            animationBox = new Rectangle(0, 0, 75, 116);
            width /= 9;
            height /= 9;
            aggroRadius = 250;
            damageToPlayer -= 1;
            life = 5;
            enemySpeed = 1.5f;
            offset = new Vector2(width / 2, height / 2);
        }

        public override void Update(GameTime gameTime)
        {
            boundingBox = new Rectangle((int)pos.X - width / 2, (int)pos.Y - height / 2, width, height);
            feetBox = new Rectangle((int)pos.X - (int)49, (int)pos.Y + (113 - 4) - (int)offset.Y, width, height - (height - 4));
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(tex, boundingBox, Color.Blue);
            base.Draw(spriteBatch);
        }
    }
}
