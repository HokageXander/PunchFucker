using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Johnny_Punch
{
    abstract class Enemy : Movables
    {
        protected int aggroRadius;
        protected Vector2 velocity;
        public int damageToPlayer;
        public Enemy(Texture2D tex, Vector2 pos)
            : base(tex,pos)
        {
            //this.boundingBox = sourceRect;
            
            

        }
        public override void Update(GameTime gameTime)
        {
            boundingBox = new Rectangle((int)pos.X - width / 2, (int)pos.Y - height / 2, width, height);
            feetBox = new Rectangle((int)pos.X - width / 2, (int)pos.Y + (height - 4) - height / 2, width, height - (height - 4));

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, animationBox, Color.White, 0f, offset, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(tex, feetBox, Color.Red);
            //spriteBatch.Draw(tex, boundingBox, Color.Red);
        }

        public void Aggro(Player player)
        {
            if (Vector2.Distance(pos, player.pos) < aggroRadius && Vector2.Distance(pos, player.pos) > 20)
            {
                Vector2 direction = new Vector2(player.pos.X, player.pos.Y) - this.pos;
                direction.Normalize();
                velocity.X = 1 * direction.X;
                velocity.Y = 3 * direction.Y;
                pos += velocity;
            }
            //if (player.feetBox.Y - feetBox.Y <= 20)
            if(Vector2.Distance(pos, player.pos) < 20)
            {
                //punch that mther fycer
                velocity.X = 0;
                velocity.Y = 0;
                pos += velocity;
            }
        }
    }
}
